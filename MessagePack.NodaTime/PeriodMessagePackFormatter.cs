// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.Formatters;
using NodaTime;
using NodaTime.Text;
using System;

namespace MessagePack.NodaTime
{
    public sealed class PeriodAsIsoStringMessagePackFormatter : IMessagePackFormatter<Period>
    {
        public static readonly PeriodAsIsoStringMessagePackFormatter Instance = new PeriodAsIsoStringMessagePackFormatter();

        public Period Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            if (MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var s = MessagePackBinary.ReadString(bytes, offset, out readSize);
            if (s.Length == 0)
                return Period.Zero;

            return PeriodPattern.Roundtrip.Parse(s).GetValueOrThrow();
        }

        public int Serialize(ref byte[] bytes, int offset, Period value, IFormatterResolver formatterResolver)
        {
            if (value == null)
                return MessagePackBinary.WriteNil(ref bytes, offset);

            return MessagePackBinary.WriteString(ref bytes, offset, PeriodPattern.Roundtrip.Format(value));
        }
    }

    public sealed class PeriodAsIntArrayMessagePackFormatter : IMessagePackFormatter<Period>
    {
        public static readonly PeriodAsIntArrayMessagePackFormatter Instance = new PeriodAsIntArrayMessagePackFormatter();

        public Period Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            if (MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }
            var startOffset = offset;

            var count = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
            offset += readSize;
            if (count != 10) throw new InvalidOperationException("Invalid array count");

            var years = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;
            var months = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;
            var weeks = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;
            var days = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;
            var hours = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
            offset += readSize;
            var minutes = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
            offset += readSize;
            var seconds = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
            offset += readSize;
            var milliseconds = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
            offset += readSize;
            var ticks = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
            offset += readSize;
            var nano = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
            offset += readSize;


            readSize = offset - startOffset;

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
            }.Build();
        }

        public int Serialize(ref byte[] bytes, int offset, Period value, IFormatterResolver formatterResolver)
        {
            if (value == null)
                return MessagePackBinary.WriteNil(ref bytes, offset);

            var startOffset = offset;

            offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 10);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Years);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Months);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Weeks);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Days);
            offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.Hours);
            offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.Minutes);
            offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.Seconds);
            offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.Milliseconds);
            offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.Ticks);
            offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.Nanoseconds);

            return offset - startOffset;
        }
    }
}