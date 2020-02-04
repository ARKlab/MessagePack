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

        public LocalTime Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var nanos = reader.ReadInt64();
            return LocalTime.Midnight.PlusNanoseconds(nanos);
        }

        public void Serialize(ref MessagePackWriter writer, LocalTime value, MessagePackSerializerOptions options)
        {
            writer.WriteInt64(value.NanosecondOfDay);
        }
    }
}
