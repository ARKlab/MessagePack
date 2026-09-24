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
    public class ZonedDateTimeMessagePackFormatterTest
    {
        [Test]
        public async Task ZonedDateTimeTest()
        {
            Instant inst = Instant.FromDateTimeUtc(DateTime.UtcNow);
            ZonedDateTime zoned = new ZonedDateTime(inst, DateTimeZone.Utc);
            await Assert.That(TestTools.Convert(zoned)).IsEqualTo(zoned);
        }

        [Test]
        public async Task NullableZonedDateTimeTest()
        {
            ZonedDateTime? zoned = null;
            await Assert.That(TestTools.Convert(zoned)).IsEqualTo(zoned);
        }

        [Test]
        public async Task ZonedDateTimeArrayTest()
        {
            Instant inst = Instant.FromDateTimeUtc(DateTime.UtcNow);
            LocalDateTime ldt = LocalDateTime.FromDateTime(DateTime.Now);
            ZonedDateTime[] zoned = new ZonedDateTime[]
                { new ZonedDateTime(inst, DateTimeZone.Utc),
                new ZonedDateTime(inst, DateTimeZone.Utc),
                new ZonedDateTime(inst, DateTimeZone.Utc),
                new ZonedDateTime(inst, DateTimeZone.Utc),
                new ZonedDateTime(inst, DateTimeZone.Utc)
            };
            await Assert.That(TestTools.Convert(zoned)).IsEquivalentTo(zoned, CollectionOrdering.Matching);
        }        
        
        [Test]
        public async Task NullableZonedDateTimeArrayTest()
        {
            ZonedDateTime?[] zoned = new ZonedDateTime?[] {
                null,
                null,
                null,
                null,
                null
            };
            await Assert.That(TestTools.Convert(zoned)).IsEquivalentTo(zoned, CollectionOrdering.Matching);
        }
    }
}
