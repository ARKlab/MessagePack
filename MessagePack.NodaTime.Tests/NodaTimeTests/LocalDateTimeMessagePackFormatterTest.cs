// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.NodaTime.Tests.Helpers;
using NodaTime;
using System;
using System.Threading.Tasks;
using TUnit.Assertions.Enums;

namespace MessagePack.NodaTime.Tests
{
    public class LocalDateTimeAsDateTimeMessagePackFormatterTest
    {
        [Test]
        public async Task LocalDateTimeAsDateTimeTest()
        {
            LocalDateTime ldt = LocalDateTime.FromDateTime(DateTime.Now);
            await Assert.That(TestTools.Convert(ldt)).IsEqualTo(ldt);
        }

        [Test]
        public async Task NullableLocalDateTimeAsDateTimeTest()
        {
            LocalDateTime? ldt = null;
            await Assert.That(TestTools.Convert(ldt)).IsEqualTo(ldt);
        }

        [Test]
        public async Task LocalDateTimeArrayTest()
        {
            LocalDateTime[] ldt =
                { LocalDateTime.FromDateTime(DateTime.Now.AddDays(3)),
                LocalDateTime.FromDateTime(new DateTime()),
                LocalDateTime.FromDateTime(DateTime.Now.AddTicks(500)),
                LocalDateTime.FromDateTime(DateTime.Now),
                LocalDateTime.FromDateTime(new DateTime(2010,10,10))
            };
            await Assert.That(TestTools.Convert(ldt)).IsEquivalentTo(ldt, CollectionOrdering.Matching);
        }

        [Test]
        public async Task NullableLocalDateTimeArrayTest()
        {
            LocalDateTime?[] ldt = new LocalDateTime?[] {
                null,
                null,
                null,
                null,
                null
            };
            await Assert.That(TestTools.Convert(ldt)).IsEquivalentTo(ldt, CollectionOrdering.Matching);
        }

        [Test]
        public async Task LocalDateTimeToLocalDateWithTimeLoss()
        {
            LocalDateTime ldt = new LocalDateTime(2018, 5, 15, 1, 0, 0).PlusTicks(1);
            var bin = MessagePackSerializer.Serialize(ldt);

            await TestTools.ThrowsInner<InvalidOperationException>(() =>
            (MessagePackSerializer.Deserialize<LocalDate>(bin)));
        }

        [Test]
        public async Task LocalDateTimeToLocalDateTimeWithNanosecondsLoss()
        {
            //nanosecond accuracy lost in datetime conversion --> ReadMe
            LocalDateTime ldt = new LocalDateTime(2018, 5, 15, 0, 0, 0).PlusNanoseconds(1);
            var bin = MessagePackSerializer.Serialize(ldt);
            var res = TestTools.Convert(ldt);

            await Assert.That(res).IsNotEqualTo(ldt);
        }
    }
}
