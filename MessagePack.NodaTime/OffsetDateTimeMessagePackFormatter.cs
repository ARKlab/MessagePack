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
        
        public OffsetDateTime Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var count = reader.ReadArrayHeader();
            
            if (count != 2) 
                throw new InvalidOperationException("Invalid array count");

            var dt = LocalDateTimeAsDateTimeMessagePackFormatter.Instance.Deserialize(ref reader, options);
            var off = OffsetMessagePackFormatter.Instance.Deserialize(ref reader, options);

            return new OffsetDateTime(dt, off);
        }

        public void Serialize(ref MessagePackWriter writer, OffsetDateTime value, MessagePackSerializerOptions options)
        {
            var dt = value.LocalDateTime;
            var off = value.Offset;

            writer.WriteArrayHeader(2);
            LocalDateTimeAsDateTimeMessagePackFormatter.Instance.Serialize(ref writer, dt, options);
            OffsetMessagePackFormatter.Instance.Serialize(ref writer, off, options);
        }
    }
}
