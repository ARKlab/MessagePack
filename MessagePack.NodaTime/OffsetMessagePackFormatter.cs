// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.Formatters;
using NodaTime;

namespace MessagePack.NodaTime
{
    public sealed class OffsetMessagePackFormatter : IMessagePackFormatter<Offset>
    {
        public static readonly OffsetMessagePackFormatter Instance = new OffsetMessagePackFormatter();

        public Offset Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var seconds = reader.ReadInt32();
            return Offset.FromSeconds(seconds);
        }

        public void Serialize(ref MessagePackWriter writer, Offset value, MessagePackSerializerOptions options)
        {
            writer.WriteInt32(value.Seconds);
        }
    }
}
