// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.Formatters;
using NodaTime;
using System;

namespace MessagePack.NodaTime
{
    public sealed class ZonedDateTimeMessagePackFormatter : IMessagePackFormatter<ZonedDateTime>
    {
        public static readonly ZonedDateTimeMessagePackFormatter Instance = new ZonedDateTimeMessagePackFormatter();

        public ZonedDateTime Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            var startOffset = offset;

            var count = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
            offset += readSize;
            if (count != 3) throw new InvalidOperationException("Invalid array count");

            var dt = LocalDateTimeAsDateTimeMessagePackFormatter.Instance.Deserialize(bytes, offset, formatterResolver, out readSize);
            offset += readSize;
            var off = OffsetMessagePackFormatter.Instance.Deserialize(bytes, offset, formatterResolver, out readSize);
            offset += readSize;
            var zoneId = MessagePackBinary.ReadString(bytes, offset, out readSize);
            offset += readSize;

            readSize = offset - startOffset;

            return new ZonedDateTime(dt, DateTimeZoneProviders.Serialization[zoneId], off);
        }

        public int Serialize(ref byte[] bytes, int offset, ZonedDateTime value, IFormatterResolver formatterResolver)
        {
            var dt = value.LocalDateTime;
            var off = value.Offset;
            var zoneId = value.Zone.Id;

            var startOffset = offset;

            offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 3);
            offset += LocalDateTimeAsDateTimeMessagePackFormatter.Instance.Serialize(ref bytes, offset, dt, formatterResolver);
            offset += OffsetMessagePackFormatter.Instance.Serialize(ref bytes, offset, off, formatterResolver);
            offset += MessagePackBinary.WriteString(ref bytes, offset, zoneId);

            return offset - startOffset;
        }
    }

}
