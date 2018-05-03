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
    public class InstantMessagePackFormatterTest
    {
        [Fact]
        public void InstantValueTest()
        {
            Instant inst = Instant.FromDateTimeUtc(DateTime.UtcNow);
            Assert.Equal(TestTools.Convert(inst), inst);
        }

        [Fact]
        public void NullableInstantValueTest()
        {
            Instant? inst = null;
            Assert.Equal(TestTools.Convert(inst), inst);
        }                
       
        [Fact]
        public void InstantArrayTest()
        {
            Instant[] inst =
                { Instant.FromDateTimeUtc(DateTime.UtcNow),
                Instant.FromDateTimeUtc(DateTime.UtcNow),
                Instant.FromDateTimeUtc(DateTime.UtcNow),
                Instant.FromDateTimeUtc(DateTime.UtcNow),
                Instant.FromDateTimeUtc(DateTime.UtcNow),
            };
            Assert.Equal(TestTools.Convert(inst), inst);
        }

        [Fact]
        public void NullableInstantArrayTest()
        {
            Instant?[] inst = new Instant?[] {
                null,
                null,
                null,
                null,
                null
            };
            Assert.Equal(TestTools.Convert(inst), inst);
        }       
    }
}
