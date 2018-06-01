// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.Formatters;
using NodaTime;
using System;

namespace MessagePack.NodaTime
{
    public sealed class OffsetDateTimeMessagePackFormatter : IMessagePackFormatter<OffsetDateTime>
    {
        public static readonly OffsetDateTimeMessagePackFormatter Instance = new OffsetDateTimeMessagePackFormatter();

        public OffsetDateTime Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            var startOffset = offset;

            var count = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
            offset += readSize;
            if (count != 2) throw new InvalidOperationException("Invalid array count");

            var dt = LocalDateTimeAsDateTimeMessagePackFormatter.Instance.Deserialize(bytes, offset, formatterResolver, out readSize);
            offset += readSize;
            var off = OffsetMessagePackFormatter.Instance.Deserialize(bytes, offset, formatterResolver, out readSize);
            offset += readSize;

            readSize = offset - startOffset;

            return new OffsetDateTime(dt, off);
        }

        public int Serialize(ref byte[] bytes, int offset, OffsetDateTime value, IFormatterResolver formatterResolver)
        {
            var dt = value.LocalDateTime;
            var off = value.Offset;

            var startOffset = offset;

            offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
            offset += LocalDateTimeAsDateTimeMessagePackFormatter.Instance.Serialize(ref bytes, offset, dt, formatterResolver);
            offset += OffsetMessagePackFormatter.Instance.Serialize(ref bytes, offset, off, formatterResolver);

            return offset - startOffset;
        }
    }
}
