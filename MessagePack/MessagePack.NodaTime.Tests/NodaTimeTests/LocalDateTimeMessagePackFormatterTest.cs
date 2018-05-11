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
    public class LocalDateTimeAsDateTimeMessagePackFormatterTest
    {
        [Fact]
        public void LocalDateTimeAsDateTimeTest()
        {
            LocalDateTime ldt = LocalDateTime.FromDateTime(DateTime.Now);
            Assert.Equal(TestTools.Convert(ldt), ldt);
        }

        [Fact]
        public void NullableLocalDateTimeAsDateTimeTest()
        {
            LocalDateTime? ldt = null;
            Assert.Equal(TestTools.Convert(ldt), ldt);
        }
        
        [Fact]
        public void LocalDateTimeArrayTest()
        {
            LocalDateTime[] ldt =
                { LocalDateTime.FromDateTime(DateTime.Now.AddDays(3)),
                LocalDateTime.FromDateTime(new DateTime()),
                LocalDateTime.FromDateTime(DateTime.Now.AddTicks(500)),
                LocalDateTime.FromDateTime(DateTime.Now),
                LocalDateTime.FromDateTime(new DateTime(2010,10,10))
            };
            Assert.Equal(TestTools.Convert(ldt), ldt);
        }

        [Fact]
        public void NullableLocalDateTimeArrayTest()
        {
            LocalDateTime?[] ldt = new LocalDateTime?[] {
                null,
                null,
                null,
                null,
                null
            };
            Assert.Equal(TestTools.Convert(ldt), ldt);
        }
    }
}
