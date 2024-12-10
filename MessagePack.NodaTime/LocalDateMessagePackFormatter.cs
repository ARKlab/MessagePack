// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.Formatters;
using NodaTime;
using System;

namespace MessagePack.NodaTime
{
    // Not interoperable
    [ExcludeFormatterFromSourceGeneratedResolver]
    public sealed class LocalDateAsExtMessagePackFormatter : IMessagePackFormatter<LocalDate>
    {
        public static readonly LocalDateAsExtMessagePackFormatter Instance = new LocalDateAsExtMessagePackFormatter();

        // copied from Nodatime code as this details are not exposed even if they would be great for serialization
        internal const int CalendarBits = 6; // Up to 64 calendars.
        internal const int DayBits = 6;   // Up to 64 days in a month.
        internal const int MonthBits = 5; // Up to 32 months per year.
        internal const int YearBits = 15; // 32K range; only need -10K to +10K.
        internal const int CalendarDayBits = CalendarBits + DayBits;
        internal const int CalendarDayMonthBits = CalendarDayBits + MonthBits;

        internal const int CalendarMask = (1 << CalendarBits) - 1;
        internal const int DayMask = ((1 << DayBits) - 1) << CalendarBits;
        internal const int MonthMask = ((1 << MonthBits) - 1) << CalendarDayBits;
        internal const int YearMask = ((1 << YearBits) - 1) << CalendarDayMonthBits;

        public void Serialize(ref MessagePackWriter writer, LocalDate value, MessagePackSerializerOptions options)
        {
            uint v = 0;
            unchecked
            {
                v = (uint)(
                        ((value.Year - 1) << CalendarDayMonthBits) |
                        ((value.Month - 1) << CalendarDayBits) |
                        ((value.Day - 1) << CalendarBits) |
                        0
                    );
            }
            byte[] b = { MessagePackCode.FixExt4
                          , unchecked((byte)NodatimeMessagePackExtensionTypeCode.LocalDate)
                          , unchecked((byte)(v >> 24))
                          , unchecked((byte)(v >> 16))
                          , unchecked((byte)(v >> 8))
                          , unchecked((byte)v)
                };

            writer.WriteRaw(b);
        }

        public LocalDate Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var v = reader.ReadInt32();
            var year = unchecked(((v & YearMask) >> CalendarDayMonthBits) + 1);
            var month = unchecked(((v & MonthMask) >> CalendarDayBits) + 1);
            var day = unchecked(((v & DayMask) >> CalendarBits) + 1);

            return new LocalDate(year, month, day);
        }
    }

    // Supported
    public sealed class LocalDateAsDatetimeMessagePackFormatter : IMessagePackFormatter<LocalDate>
    {
        public static readonly LocalDateAsDatetimeMessagePackFormatter Instance = new LocalDateAsDatetimeMessagePackFormatter();

        public LocalDate Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var dt = reader.ReadDateTime();
            LocalDateTime ldt = LocalDateTime.FromDateTime(dt);

            if (ldt.TimeOfDay != LocalTime.Midnight)
                throw new InvalidOperationException($"Local Date should not contain time part. Found {ldt.TimeOfDay} instead of midnight");

            return LocalDate.FromDateTime(dt);
        }

        public void Serialize(ref MessagePackWriter writer, LocalDate value, MessagePackSerializerOptions options)
        {
            writer.Write(DateTime.SpecifyKind(value.ToDateTimeUnspecified(), DateTimeKind.Utc));
        }
    }
}
