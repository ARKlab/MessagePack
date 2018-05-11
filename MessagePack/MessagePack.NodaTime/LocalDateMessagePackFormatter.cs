// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.Formatters;
using NodaTime;
using System;

namespace MessagePack.NodaTime
{
    // Not supported
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

        public LocalDate Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            var v = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            var year = unchecked(((v & YearMask) >> CalendarDayMonthBits) + 1);
            var month = unchecked(((v & MonthMask) >> CalendarDayBits) + 1);
            var day = unchecked(((v & DayMask) >> CalendarBits) + 1);

            return new LocalDate(year, month, day);
        }

        public int Serialize(ref byte[] bytes, int offset, LocalDate value, IFormatterResolver formatterResolver)
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

            MessagePackBinary.EnsureCapacity(ref bytes, offset, 6);
            bytes[offset] = MessagePackCode.FixExt4;
            bytes[offset + 1] = unchecked((byte)NodatimeMessagePackExtensionTypeCode.LocalDate);
            bytes[offset + 2] = unchecked((byte)(v >> 24));
            bytes[offset + 3] = unchecked((byte)(v >> 16));
            bytes[offset + 4] = unchecked((byte)(v >> 8));
            bytes[offset + 5] = unchecked((byte)v);
            return 6;
        }
    }
    // Supported
    public sealed class LocalDateAsDatetimeMessagePackFormatter : IMessagePackFormatter<LocalDate>
    {
        public static readonly LocalDateAsDatetimeMessagePackFormatter Instance = new LocalDateAsDatetimeMessagePackFormatter();

        public LocalDate Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            var dt = MessagePackBinary.ReadDateTime(bytes, offset, out readSize);

            if (dt.TimeOfDay.TotalMilliseconds != 0)
                throw new InvalidOperationException($"code is invalid. code:{bytes[offset]} format:{MessagePackCode.ToFormatName(bytes[offset])}");

            return LocalDate.FromDateTime(dt);
        }

        public int Serialize(ref byte[] bytes, int offset, LocalDate value, IFormatterResolver formatterResolver)
        {
            return MessagePackBinary.WriteDateTime(ref bytes, offset, DateTime.SpecifyKind(value.ToDateTimeUnspecified(), DateTimeKind.Utc));
        }
    }
}
