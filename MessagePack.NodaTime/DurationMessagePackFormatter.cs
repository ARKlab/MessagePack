// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.Formatters;

using NodaTime;
using NodaTime.Text;

using System;

namespace MessagePack.NodaTime
{
    [ExcludeFormatterFromSourceGeneratedResolver]
    public sealed class DurationAsNanosecondsMessagePackFormatter : IMessagePackFormatter<Duration>
    {
        public static readonly DurationAsNanosecondsMessagePackFormatter Instance = new DurationAsNanosecondsMessagePackFormatter();

        public Duration Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var s = reader.ReadInt64();

            return Duration.FromNanoseconds(s);
        }

        public void Serialize(ref MessagePackWriter writer, Duration value, MessagePackSerializerOptions options)
        {
            writer.Write(value.ToInt64Nanoseconds());
        }
    }

    public sealed class DurationAsBclTicksMessagePackFormatter : IMessagePackFormatter<Duration>
    {
        public static readonly DurationAsBclTicksMessagePackFormatter Instance = new DurationAsBclTicksMessagePackFormatter();

        public Duration Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var s = reader.ReadInt64();

            return Duration.FromTicks(s);
        }

        public void Serialize(ref MessagePackWriter writer, Duration value, MessagePackSerializerOptions options)
        {
            writer.Write(value.BclCompatibleTicks);
        }
    }
}