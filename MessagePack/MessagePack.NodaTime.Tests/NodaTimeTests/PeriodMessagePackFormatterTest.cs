// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.NodaTime.Tests.Helpers;
using NodaTime;
using Xunit;

namespace MessagePack.NodaTime.Tests
{
    [Collection("ResolverCollection")]
    public class PeriodMessagePackFormatterTest
    {
        [Fact]
        public void PeriodTest()
        {
            Period p = Period.FromDays(1);
            Assert.Equal(TestTools.Convert(p), p);
        }        

        [Fact]
        public void PeriodArrayTest()
        {
            Period[] p = new Period[]
                { Period.FromDays(1),
                Period.FromMonths(2),
                Period.FromDays(1),
                Period.FromNanoseconds(5),
                Period.FromHours(3)
            };
            Assert.Equal(TestTools.Convert(p), p);
        }
     }
}
