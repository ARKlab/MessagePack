// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.NodaTime.Tests.Helpers;
using NodaTime;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace MessagePack.NodaTime.Tests
{
    [Collection("ResolverCollection")]
    public class SystemDateTimeTests
    {
        [Fact]
        public void LocalDateTimeToDateTime()
        {
            DateTime dt = new DateTime(2000, 1, 1, 1, 22, 33);
            LocalDateTime ldt = LocalDateTime.FromDateTime(dt);

            var localDateTimeBinary = MessagePackSerializer.Serialize(ldt);
            var resultDateTime = MessagePackSerializer.Deserialize<DateTime>(localDateTimeBinary);

            Assert.Equal(dt, resultDateTime);
        }

        [Fact]
        public void DateTimeToLocalDateTime()
        {
            var ldt = new LocalDateTime(2018, 5, 15, 0, 0);
            var dt = DateTime.SpecifyKind(ldt.ToDateTimeUnspecified(), DateTimeKind.Utc);

            var bin = MessagePackSerializer.Serialize(dt);
            var res = MessagePackSerializer.Deserialize<LocalDateTime>(bin);

            Assert.Equal(ldt, res);
        }

        [Fact]
        public void LocalDateTimeToLocalDate()
        {
            LocalDateTime ldt = new LocalDateTime(2016, 08, 21, 0, 0, 0, 0);

            var bin = MessagePackSerializer.Serialize(ldt);
            var res = MessagePackSerializer.Deserialize<LocalDate>(bin);

            Assert.Equal(ldt.Date, res);
        }

        [Fact(Skip = "Needs investigation")]
        public void LocalDateTimeToLocalDateFailing()
        {
            LocalDateTime ldt = new LocalDateTime(2016, 08, 21, 0, 0, 0, 0).PlusNanoseconds(1);

            var bin = MessagePackSerializer.Serialize(ldt);
            var res = MessagePackSerializer.Deserialize<LocalDate>(bin);

            Assert.Equal(ldt.Date, res);
        }

        [Fact]
        public void DateTimeToLocalDate()
        {
            DateTime dt = new DateTime(1986, 12, 11, 0, 0, 0);

            var bin = MessagePackSerializer.Serialize(dt);
            var res = MessagePackSerializer.Deserialize<LocalDate>(bin);

            Assert.Equal(dt.Date, res.ToDateTimeUnspecified());
        }

        [Fact]
        public void LocalDateToLocalDateTime()
        {
            LocalDate ld = new LocalDate(2000, 1, 1);

            LocalDateTime ldt = LocalDateTime.FromDateTime(new DateTime(2000, 1, 1));

            var bin = MessagePackSerializer.Serialize(ld);
            var res = MessagePackSerializer.Deserialize<LocalDateTime>(bin);

            Assert.Equal(ldt, res);
        }

        [Fact]
        public void DateTimeToLocalDateWithPrecisionLoss()
        {
            DateTime dt = new DateTime(2000, 1, 1, 0, 0, 1);

            Assert.Throws<InvalidOperationException>(() => 
            (MessagePackSerializer.Deserialize<LocalDate>(MessagePackSerializer.Serialize(dt))));
        }

        [Fact]
        public void LocalDateTimeToLocalDateWithPrecisionLoss()
        {
            LocalDateTime ldt = new LocalDateTime(2000, 1, 1, 0, 0, 0, 1);

            Assert.Throws<InvalidOperationException>(() =>
            (MessagePackSerializer.Deserialize<LocalDate>(MessagePackSerializer.Serialize(ldt))));            
        }

        [Fact(Skip = "Fails due to Time being converted to UTC, goes back an hour")]
        public void DateTimeToLocalDateTime1()
        {            
            var ldt = new LocalDateTime(2018, 5, 15, 0, 0, 0);
            var dt = new DateTime(2018, 5, 15, 0, 0, 0);

            var bin = MessagePackSerializer.Serialize(dt);
            var res = MessagePackSerializer.Deserialize<LocalDateTime>(bin);

            Assert.Equal(ldt, res);
        }
    }
}
