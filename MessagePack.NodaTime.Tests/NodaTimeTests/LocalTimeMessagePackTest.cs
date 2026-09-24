// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.NodaTime.Tests.Helpers;
using NodaTime;
using System.Threading.Tasks;
using TUnit.Assertions.Enums;

namespace MessagePack.NodaTime.Tests
{
    public class LocalTimeMessagePackFormatterTest
    {
        [Test]
        public async Task LocalTimeTest()
        {
            LocalTime t = LocalTime.FromSecondsSinceMidnight(1);
            await Assert.That(TestTools.Convert(t)).IsEqualTo(t);
        }

        [Test]
        public async Task NullableLocalTimeTest()
        {
            LocalTime? t = null;
            await Assert.That(TestTools.Convert(t)).IsEqualTo(t);
        }        

        [Test]
        public async Task LocalTimeArrayTest()
        {
            LocalTime[] lt =
                { LocalTime.FromTicksSinceMidnight(4000),
                LocalTime.FromSecondsSinceMidnight(10000),
                LocalTime.FromHourMinuteSecondTick(20,10,1,13),
                new LocalTime(),
                LocalTime.FromSecondsSinceMidnight(1)
            };
            await Assert.That(TestTools.Convert(lt)).IsEquivalentTo(lt, CollectionOrdering.Matching);
        }

        [Test]
        public async Task NullableLocalTimeArrayTest()
        {
            LocalTime?[] lt = new LocalTime?[] {
                null,
                null,
                null,
                null,
                null
            };
            await Assert.That(TestTools.Convert(lt)).IsEquivalentTo(lt, CollectionOrdering.Matching);
        }        
    }
}
