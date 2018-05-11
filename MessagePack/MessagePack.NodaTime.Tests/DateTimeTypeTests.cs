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
    public class DateTimeTypeTests
    {
        [Fact]
        public void LocalDateTimeToDateTime()
        {
            DateTime dt = new DateTime(2000, 1, 1, 0, 0, 0);
            LocalDateTime ldt = LocalDateTime.FromDateTime(dt);

            var localDateTimeBinary = MessagePackSerializer.Serialize(ldt);
            var resultDateTime = MessagePackSerializer.Deserialize<DateTime>(localDateTimeBinary);

            Assert.Equal(dt, resultDateTime);
        }

        [Fact]
        public void DateTimeToLocalDateTime()
        {
            DateTime dt = new DateTime(2000, 1, 1, 0, 0, 0);
            LocalDateTime ldt = LocalDateTime.FromDateTime(dt);

            var bin = MessagePackSerializer.Serialize(dt);
            var res = MessagePackSerializer.Deserialize<LocalDateTime>(bin);

            Assert.Equal(ldt, res);
        }

        [Fact]
        public void LocalDateTimeToLocalDate()
        {
            LocalDateTime ldt = new LocalDateTime(2000, 1, 1, 0, 0, 0, 0);
            var bin = MessagePackSerializer.Serialize(ldt);

            var res = MessagePackSerializer.Deserialize<LocalDate>(bin);
            Assert.Equal(ldt.Date, res);
        }

        [Fact]
        public void DateTimeToLocalDate()
        {
            DateTime dt = new DateTime(2000, 1, 1, 0, 0, 0);

            var bin = MessagePackSerializer.Serialize(dt);

            var res = MessagePackSerializer.Deserialize<LocalDate>(bin);
            Assert.Equal(dt, res.ToDateTimeUnspecified());
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
            Exception ex = null;

            var bin = MessagePackSerializer.Serialize(dt);

            try
            {
                var res = MessagePackSerializer.Deserialize<LocalDate>(bin);
            }
            catch (Exception e)
            {
                ex = e;
            }

            Assert.Equal(typeof(InvalidOperationException), ex.GetType());
        }

        [Fact]
        public void LocalDateTimeToLocalDateWithPrecisionLoss()
        {
            LocalDateTime ldt = new LocalDateTime(2000, 1, 1, 0, 0, 0, 1);
            Exception ex = null;
            var bin = MessagePackSerializer.Serialize(ldt);            

            try
            {
                var res = MessagePackSerializer.Deserialize<LocalDate>(bin);
            }
            catch (Exception e)
            {

                ex = e;
            }
            Assert.Equal(typeof(InvalidOperationException), ex.GetType());
        }

    }
}
