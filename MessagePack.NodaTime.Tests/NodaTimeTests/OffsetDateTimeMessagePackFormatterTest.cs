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
    public class OffsetDateTimeMessagePackFormatterTest
    {
        [Test]
        public async Task OffsetDateTimeTest()
        {
            OffsetDateTime offSet = new OffsetDateTime().PlusHours(3).WithOffset(Offset.FromHours(6));
            await Assert.That(TestTools.Convert(offSet)).IsEqualTo(offSet);
        }

        [Test]
        public async Task NullableOffsetDateTimeTest()
        {
            OffsetDateTime? offSet = null;
            await Assert.That(TestTools.Convert(offSet)).IsEqualTo(offSet);
        }
        
        [Test]
        public async Task OffsetDateTimeArrayTest()
        {
            OffsetDateTime[] offSet = new OffsetDateTime[]
                { new OffsetDateTime().WithOffset(Offset.FromHours(2)),
                new OffsetDateTime(),
                new OffsetDateTime(LocalDateTime.FromDateTime(DateTime.UtcNow).PlusNanoseconds(200), Offset.FromHours(1)),
                new OffsetDateTime().PlusMinutes(10),
                new OffsetDateTime().PlusHours(3).WithOffset(Offset.FromHours(6))
            };

            var aaa = TestTools.Convert(offSet);

            await Assert.That(TestTools.Convert(offSet)).IsEquivalentTo(offSet, CollectionOrdering.Matching);
        }

        [Test]
        public async Task NullableOffsetDateTimeArrayTest()
        {
            OffsetDateTime?[] offSet = new OffsetDateTime?[] {
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
