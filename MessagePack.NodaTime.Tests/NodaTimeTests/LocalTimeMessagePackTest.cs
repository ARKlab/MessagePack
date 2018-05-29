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
                { LocalTime.FromTicksSinceMidnight(4000),
                LocalTime.FromSecondsSinceMidnight(10000),
                LocalTime.FromHourMinuteSecondTick(20,10,1,13),
                new LocalTime(),
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
