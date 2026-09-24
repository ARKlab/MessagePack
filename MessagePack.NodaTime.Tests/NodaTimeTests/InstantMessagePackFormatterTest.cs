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
    public class InstantMessagePackFormatterTest
    {
        [Test]
        public async Task InstantValueTest()
        {
            Instant inst = Instant.FromDateTimeUtc(DateTime.UtcNow);
            await Assert.That(TestTools.Convert(inst)).IsEqualTo(inst);
        }

        [Test]
        public async Task NullableInstantValueTest()
        {
            Instant? inst = null;
            await Assert.That(TestTools.Convert(inst)).IsEqualTo(inst);
        }                

        [Test]
        public async Task InstantArrayTest()
        {
            Instant[] inst =
                { Instant.FromDateTimeUtc(DateTime.UtcNow.AddHours(13)),
                Instant.FromDateTimeUtc(DateTime.UtcNow.AddMinutes(54)),
                Instant.FromDateTimeUtc(DateTime.UtcNow.AddYears(1)),
                Instant.FromDateTimeUtc(DateTime.UtcNow.AddSeconds(33)),
                Instant.FromDateTimeUtc(DateTime.UtcNow),
            };
            await Assert.That(TestTools.Convert(inst)).IsEquivalentTo(inst, CollectionOrdering.Matching);
        }

        [Test]
        public async Task NullableInstantArrayTest()
        {
            Instant?[] inst = new Instant?[] {
                null,
                null,
                null,
                null,
                null
            };
            await Assert.That(TestTools.Convert(inst)).IsEquivalentTo(inst, CollectionOrdering.Matching);
        }       
    }
}
