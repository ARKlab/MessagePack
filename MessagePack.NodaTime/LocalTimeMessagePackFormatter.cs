// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.Formatters;
using NodaTime;

namespace MessagePack.NodaTime
{
    public sealed class LocalTimeAsNanosecondsMessagePackFormatter : IMessagePackFormatter<LocalTime>
    {
        public static readonly LocalTimeAsNanosecondsMessagePackFormatter Instance = new LocalTimeAsNanosecondsMessagePackFormatter();

        public LocalTime Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            var nanos = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
            return LocalTime.Midnight.PlusNanoseconds(nanos);
        }

        public int Serialize(ref byte[] bytes, int offset, LocalTime value, IFormatterResolver formatterResolver)
        {
            return MessagePackBinary.WriteInt64(ref bytes, offset, value.NanosecondOfDay);
        }
    }
}
