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

        public Offset Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            var seconds = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            return Offset.FromSeconds(seconds);
        }

        public int Serialize(ref byte[] bytes, int offset, Offset value, IFormatterResolver formatterResolver)
        {
            return MessagePackBinary.WriteInt32(ref bytes, offset, value.Seconds);
        }
    }
}
