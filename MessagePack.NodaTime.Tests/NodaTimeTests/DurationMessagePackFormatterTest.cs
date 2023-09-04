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
    public class DurationMessagePackFormatterTest
    {
        [Fact]
        public void DurationTest()
        {
            var d = Duration.FromDays(1);
            Assert.Equal(TestTools.Convert(d), d);
        }

        [Fact]
        public void DurationArrayTest()
        {
            var p = new Duration[]
            { 
                Duration.FromDays(1),
                Duration.FromNanoseconds(100),
            };
            Assert.Equal(TestTools.Convert(p), p);
        }
    }
}
