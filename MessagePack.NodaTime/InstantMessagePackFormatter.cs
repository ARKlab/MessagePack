// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.Formatters;
using NodaTime;

namespace MessagePack.NodaTime
{
    public sealed class InstantMessagePackFormatter : IMessagePackFormatter<Instant>
    {
        public static readonly InstantMessagePackFormatter Instance = new InstantMessagePackFormatter();

        public Instant Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var dt = reader.ReadDateTime();
            return Instant.FromDateTimeUtc(dt);
        }

        public void Serialize(ref MessagePackWriter writer, Instant value, MessagePackSerializerOptions options)
        {
            writer.Write(value.ToDateTimeUtc());
        }
    }
}