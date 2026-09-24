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
    public class DurationMessagePackFormatterTest
    {
        [Test]
        public async Task DurationTest()
        {
            var d = Duration.FromDays(1);
            await Assert.That(TestTools.Convert(d)).IsEqualTo(d);
        }

        [Test]
        public async Task DurationArrayTest()
        {
            var p = new Duration[]
            { 
                Duration.FromDays(1),
                Duration.FromNanoseconds(100),
            };
            await Assert.That(TestTools.Convert(p)).IsEquivalentTo(p, CollectionOrdering.Matching);
        }
    }
}
