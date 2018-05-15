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

        [Fact]
        public void LocalDateTimeToLocalDateWithTimeLoss()
        {
            LocalDateTime ldt = new LocalDateTime(2018, 5, 15, 1, 0, 0).PlusTicks(1);
            var bin = MessagePackSerializer.Serialize(ldt);

            Assert.Throws<InvalidOperationException>(() => (MessagePackSerializer.Deserialize<LocalDate>(bin)));
        }

        [Fact]
        public void LocalDateTimeToLocalDateTimeWithNanosecondsLoss()
        {
            //nanosecond accuracy lost in datetime conversion --> ReadMe
            LocalDateTime ldt = new LocalDateTime(2018, 5, 15, 0, 0, 0).PlusNanoseconds(1);
            var bin = MessagePackSerializer.Serialize(ldt);
            var res = TestTools.Convert(ldt);

            Assert.NotEqual(ldt, res);
        }
    }
}
