// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.NodaTime.Tests.Helpers;
using NodaTime;
using System;
using Xunit;

namespace MessagePack.NodaTime.Tests
{
    [Collection("ResolverCollection")]
    public class OffsetDateTimeMessagePackFormatterTest
    {
        [Fact]
        public void OffsetDateTimeTest()
        {
            OffsetDateTime offSet = new OffsetDateTime().PlusHours(3).WithOffset(Offset.FromHours(6));
            Assert.Equal(TestTools.Convert(offSet), offSet);
        }

        [Fact]
        public void NullableOffsetDateTimeTest()
        {
            OffsetDateTime? offSet = null;
            Assert.Equal(TestTools.Convert(offSet), offSet);
        }
        
        [Fact]
        public void OffsetDateTimeArrayTest()
        {
            LocalDateTime ldt = LocalDateTime.FromDateTime(DateTime.Now);
            OffsetDateTime[] offSet = new OffsetDateTime[]
                { new OffsetDateTime().WithOffset(Offset.FromHours(2)),
                new OffsetDateTime(),
                new OffsetDateTime(LocalDateTime.FromDateTime(DateTime.UtcNow).PlusNanoseconds(1), Offset.FromHours(1)),
                new OffsetDateTime().PlusMinutes(10),
                new OffsetDateTime().PlusHours(3).WithOffset(Offset.FromHours(6))
            };

            var aaa = TestTools.Convert(offSet);

            Assert.Equal(TestTools.Convert(offSet), offSet);
        }

        [Fact]
        public void NullableOffsetDateTimeArrayTest()
        {
            OffsetDateTime?[] offSet = new OffsetDateTime?[] {
                null,
                null,
                null,
                null,
                null
            };
            Assert.Equal(TestTools.Convert(offSet), offSet);
        }
        }
}
