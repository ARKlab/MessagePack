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

        public LocalDateTime Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            var dt = MessagePackBinary.ReadDateTime(bytes, offset, out readSize);
            return LocalDateTime.FromDateTime(dt);
        }

        public int Serialize(ref byte[] bytes, int offset, LocalDateTime value, IFormatterResolver formatterResolver)
        {
            return MessagePackBinary.WriteDateTime(ref bytes, offset, DateTime.SpecifyKind(value.ToDateTimeUnspecified(), DateTimeKind.Utc));
        }
    }
}
