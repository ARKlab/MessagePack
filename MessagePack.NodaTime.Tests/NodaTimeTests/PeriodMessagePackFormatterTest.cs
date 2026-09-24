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
    public class PeriodMessagePackFormatterTest
    {
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
