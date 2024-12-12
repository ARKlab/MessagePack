// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.Formatters;

using NodaTime;
using NodaTime.Text;

using System;

namespace MessagePack.NodaTime
{
    [ExcludeFormatterFromSourceGeneratedResolver]
    public sealed class PeriodAsIsoStringMessagePackFormatter : IMessagePackFormatter<Period?>
    {
        public static readonly PeriodAsIsoStringMessagePackFormatter Instance = new PeriodAsIsoStringMessagePackFormatter();

        public Period? Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.IsNil)
            {
                return null;
            }

            var s = reader.ReadString();
            if (s == null) 
                return null;
            if (s.Length == 0)
                return Period.Zero;

            return PeriodPattern.Roundtrip.Parse(s).GetValueOrThrow();
        }

        public void Serialize(ref MessagePackWriter writer, Period? value, MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }

            writer.Write(PeriodPattern.Roundtrip.Format(value));
        }
    }

    public sealed class PeriodAsIntArrayMessagePackFormatter : IMessagePackFormatter<Period?>
    {
        public static readonly PeriodAsIntArrayMessagePackFormatter Instance = new PeriodAsIntArrayMessagePackFormatter();

        public Period? Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.IsNil)
            {
                return null;
            }
            var count = reader.ReadArrayHeader();
            if (count != 10) throw new InvalidOperationException("Invalid array count");

            var years = reader.ReadInt32();
            var months = reader.ReadInt32();
            var weeks = reader.ReadInt32();
            var days = reader.ReadInt32();
            var hours = reader.ReadInt64();
            var minutes = reader.ReadInt64();
            var seconds = reader.ReadInt64();
            var milliseconds = reader.ReadInt64();
            var ticks = reader.ReadInt64();
            var nano = reader.ReadInt64();

            return new PeriodBuilder()
            {
                Years = years,
                Months = months,
                Weeks = weeks,
                Days = days,
                Hours = hours,
                Minutes = minutes,
                Seconds = seconds,
                Milliseconds = milliseconds,
                Ticks = ticks,
                Nanoseconds = nano,
            }
            .Build();
        }

        public void Serialize(ref MessagePackWriter writer, Period? value, MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }

            writer.WriteArrayHeader(10);
            writer.WriteInt32(value.Years);
            writer.WriteInt32(value.Months);
            writer.WriteInt32(value.Weeks);
            writer.WriteInt32(value.Days);
            writer.WriteInt64(value.Hours);
            writer.WriteInt64(value.Minutes);
            writer.WriteInt64(value.Seconds);
            writer.WriteInt64(value.Milliseconds);
            writer.WriteInt64(value.Ticks);
            writer.WriteInt64(value.Nanoseconds);
        }
    }
}