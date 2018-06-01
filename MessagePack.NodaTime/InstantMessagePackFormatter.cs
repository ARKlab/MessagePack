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

        public Instant Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            var dt = MessagePackBinary.ReadDateTime(bytes, offset, out readSize);
            return Instant.FromDateTimeUtc(dt);
        }
        
        public int Serialize(ref byte[] bytes, int offset, Instant value, IFormatterResolver formatterResolver)
        {
            return MessagePackBinary.WriteDateTime(ref bytes, offset, value.ToDateTimeUtc());
        }
    }
}