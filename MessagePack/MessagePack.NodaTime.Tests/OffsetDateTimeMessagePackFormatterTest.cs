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
            LocalDateTime ldt = LocalDateTime.FromDateTime(DateTime.Now);
            OffsetDateTime offSet = new OffsetDateTime(ldt, new Offset());
            Assert.Equal(TestTools.Convert(offSet), offSet);
        }

        [Fact]
        public void NullableOffsetDateTimeTest()
        {
            //LocalDateTime? ldt = null;
            OffsetDateTime? offSet = null;
            Assert.Equal(TestTools.Convert(offSet), offSet);
        }
        
        [Fact]
        public void OffsetDateTimeArrayTest()
        {
            LocalDateTime ldt = LocalDateTime.FromDateTime(DateTime.Now);
            OffsetDateTime[] offSet = new OffsetDateTime[]
                { new OffsetDateTime(ldt, new Offset()),
                new OffsetDateTime(ldt, new Offset()),
                new OffsetDateTime(ldt, new Offset()),
                new OffsetDateTime(ldt, new Offset()),
                new OffsetDateTime(ldt, new Offset())
            };
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
