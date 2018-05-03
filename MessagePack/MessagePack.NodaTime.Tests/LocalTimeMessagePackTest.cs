// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.NodaTime.Tests.Helpers;
using NodaTime;
using Xunit;

namespace MessagePack.NodaTime.Tests
{
    [Collection("ResolverCollection")]
    public class LocalTimeMessagePackFormatterTest
    {
        [Fact]
        public void LocalTimeTest()
        {
            LocalTime t = LocalTime.FromSecondsSinceMidnight(1);
            Assert.Equal(TestTools.Convert(t), t);
        }

        [Fact]
        public void NullableLocalTimeTest()
        {
            LocalTime? t = null;
            Assert.Equal(TestTools.Convert(t), t);
        }        

        [Fact]
        public void LocalTimeArrayTest()
        {
            LocalTime[] lt =
                { LocalTime.FromSecondsSinceMidnight(1),
                LocalTime.FromSecondsSinceMidnight(1),
                LocalTime.FromSecondsSinceMidnight(1),
                LocalTime.FromSecondsSinceMidnight(1),
                LocalTime.FromSecondsSinceMidnight(1)
            };
            Assert.Equal(TestTools.Convert(lt), lt);
        }

        [Fact]
        public void NullableLocalTimeArrayTest()
        {
            LocalTime?[] lt = new LocalTime?[] {
                null,
                null,
                null,
                null,
                null
            };
            Assert.Equal(TestTools.Convert(lt), lt);
        }        
    }
}
