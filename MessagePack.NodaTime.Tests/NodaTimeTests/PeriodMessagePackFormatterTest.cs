// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.Formatters;
using MessagePack.NodaTime.Tests.Helpers;
using MessagePack.Resolvers;
using NodaTime;
using System;
using System.Buffers;
using System.Threading.Tasks;
using TUnit.Assertions.Enums;

namespace MessagePack.NodaTime.Tests
{
    public class PeriodMessagePackFormatterTest
    {
        [Test]
        [Arguments(false)]
        [Arguments(true)]
        public async Task NullFollowedByPeriodRoundTrips(bool useIsoString)
        {
            IMessagePackFormatter<Period?> formatter = useIsoString
                ? PeriodAsIsoStringMessagePackFormatter.Instance
                : PeriodAsIntArrayMessagePackFormatter.Instance;
            var options = MessagePackSerializerOptions.Standard.WithResolver(CompositeResolver.Create(
                new IMessagePackFormatter[] { formatter },
                new IFormatterResolver[] { StandardResolver.Instance }));
            Period?[] periods = { null, Period.FromDays(1) };

            var bytes = MessagePackSerializer.Serialize(periods, options);
            var actual = MessagePackSerializer.Deserialize<Period?[]>(bytes, options);

            await Assert.That(actual).IsEquivalentTo(periods, CollectionOrdering.Matching);
        }

        [Test]
        [Arguments(false)]
        [Arguments(true)]
        public async Task DeserializeAdvancesPastNullAndPeriod(bool useIsoString)
        {
            IMessagePackFormatter<Period?> formatter = useIsoString
                ? PeriodAsIsoStringMessagePackFormatter.Instance
                : PeriodAsIntArrayMessagePackFormatter.Instance;
            var options = MessagePackSerializerOptions.Standard.WithResolver(CompositeResolver.Create(
                new IMessagePackFormatter[] { formatter },
                new IFormatterResolver[] { StandardResolver.Instance }));
            var period = Period.FromDays(1);
            var buffer = new ArrayBufferWriter<byte>();
            var writer = new MessagePackWriter(buffer);
            writer.WriteNil();
            formatter.Serialize(ref writer, period, options);
            writer.Flush();

            var reader = new MessagePackReader(buffer.WrittenMemory);
            var nullResult = formatter.Deserialize(ref reader, options);
            var consumedAfterNull = reader.Consumed;
            var nextType = reader.NextMessagePackType;
            var periodResult = formatter.Deserialize(ref reader, options);
            var consumedAfterPeriod = reader.Consumed;
            var atEnd = reader.End;

            await Assert.That(nullResult).IsNull();
            await Assert.That(consumedAfterNull).IsEqualTo(1L);
            await Assert.That(nextType).IsEqualTo(useIsoString ? MessagePackType.String : MessagePackType.Array);
            await Assert.That(periodResult).IsEqualTo(period);
            await Assert.That(consumedAfterPeriod).IsEqualTo((long)buffer.WrittenCount);
            await Assert.That(atEnd).IsTrue();
        }

        [Test]
        public async Task PeriodTest()
        {
            Period p = Period.FromDays(1);
            await Assert.That(TestTools.Convert(p)).IsEqualTo(p);
        }

        [Test]
        public async Task PeriodArrayTest()
        {
            var pp = new PeriodBuilder
            {
                Years = DateTime.UtcNow.Year,
                Months = DateTime.UtcNow.Month,
                Weeks = 4,
                Days = DateTime.UtcNow.Day,
                Hours = new DateTime().Hour,
                Minutes = DateTime.UtcNow.Minute,
                Seconds = new DateTime().Second,
                Milliseconds = DateTime.UtcNow.Millisecond,
                Ticks = DateTime.Now.Ticks,
                Nanoseconds = DateTime.UtcNow.Ticks / 100,
            }.Build();

            var pp1 = new PeriodBuilder
            {
                Years = DateTime.UtcNow.Year,
                Months = DateTime.UtcNow.Month,
                Weeks = 4,
                Days = new DateTime().Day               
            }.Build();

            Period[] p = new Period[]
                { Period.FromYears(1),
                pp,
                Period.FromDays(1),
                Period.FromNanoseconds(5),
                pp1
            };
            await Assert.That(TestTools.Convert(p)).IsEquivalentTo(p, CollectionOrdering.Matching);
        }
    }
}
