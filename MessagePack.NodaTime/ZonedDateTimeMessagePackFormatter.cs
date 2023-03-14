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

        public ZonedDateTime Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var count = reader.ReadArrayHeader();
            if (count != 3) 
                throw new InvalidOperationException("Invalid array count");

            var dt = LocalDateTimeAsDateTimeMessagePackFormatter.Instance.Deserialize(ref reader, options);
            var off = OffsetMessagePackFormatter.Instance.Deserialize(ref reader, options);
            var zoneId = reader.ReadString();
            if (zoneId == null)
                throw new InvalidOperationException("ZoneId, 3rd member of array, cannot be null");

            return new ZonedDateTime(dt, DateTimeZoneProviders.Tzdb[zoneId], off);
        }

        public void Serialize(ref MessagePackWriter writer, ZonedDateTime value, MessagePackSerializerOptions options)
        {
            var dt = value.LocalDateTime;
            var off = value.Offset;
            var zoneId = value.Zone.Id;

            writer.WriteArrayHeader(3);
            LocalDateTimeAsDateTimeMessagePackFormatter.Instance.Serialize(ref writer, dt, options);
            OffsetMessagePackFormatter.Instance.Serialize(ref writer, off, options);
            writer.Write(zoneId);
        }
    }

}
