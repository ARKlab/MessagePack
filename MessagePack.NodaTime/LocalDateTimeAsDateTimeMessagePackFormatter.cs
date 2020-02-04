// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.Formatters;
using NodaTime;
using System;

namespace MessagePack.NodaTime
{
    public sealed class LocalDateTimeAsDateTimeMessagePackFormatter : IMessagePackFormatter<LocalDateTime>
    {
        public static readonly LocalDateTimeAsDateTimeMessagePackFormatter Instance = new LocalDateTimeAsDateTimeMessagePackFormatter();

        public LocalDateTime Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var dt = reader.ReadDateTime();
            return LocalDateTime.FromDateTime(dt);
        }

        public void Serialize(ref MessagePackWriter writer, LocalDateTime value, MessagePackSerializerOptions options)
        {
            writer.Write(DateTime.SpecifyKind(value.ToDateTimeUnspecified(), DateTimeKind.Utc));
        }
    }
}
