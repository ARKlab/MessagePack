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
    public class ZonedDateTimeMessagePackFormatterTest
    {
        [Fact]
        public void ZonedDateTimeTest()
        {
            Instant inst = Instant.FromDateTimeUtc(DateTime.UtcNow);
            ZonedDateTime zoned = new ZonedDateTime(inst, DateTimeZone.Utc);
            Assert.Equal(TestTools.Convert(zoned), zoned);
        }

        [Fact]
        public void NullableZonedDateTimeTest()
        {
            //var inst = Instant.FromDateTimeUtc(DateTime.UtcNow);
            ZonedDateTime? zoned = null;
            Assert.Equal(TestTools.Convert(zoned), zoned);
        }

        [Fact]
        public void ZonedDateTimeArrayTest()
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
            Assert.Equal(TestTools.Convert(zoned), zoned);
        }        
        
        [Fact]
        public void NullableZonedDateTimeArrayTest()
        {
            ZonedDateTime?[] zoned = new ZonedDateTime?[] {
                null,
                null,
                null,
                null,
                null
            };
            Assert.Equal(TestTools.Convert(zoned), zoned);
        }
    }
}
