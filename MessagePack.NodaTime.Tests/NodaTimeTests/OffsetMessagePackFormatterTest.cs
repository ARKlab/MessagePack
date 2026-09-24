// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.NodaTime.Tests.Helpers;
using NodaTime;
using System.Threading.Tasks;
using TUnit.Assertions.Enums;

namespace MessagePack.NodaTime.Tests
{
    public class OffsetMessagePackFormatterTest
    {
        [Test]
        public async Task OffsetTest()
        {
            Offset offSet = Offset.FromHours(1);
            await Assert.That(TestTools.Convert(offSet)).IsEqualTo(offSet);
        }

        [Test]
        public async Task NullableOffsetTest()
        {
            Offset? offSet = null;
            await Assert.That(TestTools.Convert(offSet)).IsEqualTo(offSet);
        }               

        [Test]
        public async Task OffsetArrayTest()
        {
            Offset[] offSet = new Offset[]
                { Offset.FromHoursAndMinutes(1, 3),
                Offset.FromSeconds(80),
                Offset.FromMilliseconds(200),
                Offset.FromHours(3),
                Offset.FromNanoseconds(99)
            };
            await Assert.That(TestTools.Convert(offSet)).IsEquivalentTo(offSet, CollectionOrdering.Matching);
        }

        [Test]
        public async Task NullableOffsetArrayTest()
        {
            Offset?[] offSet = new Offset?[] {
                null,
                null,
                null,
                null,
                null
            };
            await Assert.That(TestTools.Convert(offSet)).IsEquivalentTo(offSet, CollectionOrdering.Matching);
        }        
    }
}
