// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.NodaTime.Tests.Helpers;
using NodaTime;
using System;
using Xunit;

namespace MessagePack.NodaTime.Tests.TimestampTests
{
    [Collection("ResolverCollection")]
    public class TimestampTests1
    {
        [Fact]
        public void LocalDateTimeTimestamp96_1()
        {
            LocalDateTime ldt1 = new LocalDateTime(0001, 01, 01, 00, 00, 00);
            var a = MessagePackSerializer.Serialize(ldt1); // a = timestamp96 (byte[15])
            Assert.Equal(TestTools.Convert(ldt1), ldt1);
        }
        
        [Fact]
        public void LocalDateTimeTimestamp96_2()
        {
            LocalDateTime ldt2 = new LocalDateTime(9999, 01, 01, 00, 00, 00);
            var a = MessagePackSerializer.Serialize(ldt2); // a = timestamp96 (byte[15])
            Assert.Equal(TestTools.Convert(ldt2), ldt2);
        }

        [Fact]
        public void LocalDateTimeTimestamp64()
        {
            LocalDateTime ldt3 = new LocalDateTime(2108, 01, 01, 00, 00, 00);
            var a = MessagePackSerializer.Serialize(ldt3); // a = timestamp64 (byte[10])
            Assert.Equal(TestTools.Convert(ldt3), ldt3);
        }

        [Fact]
        public void LocalDateTimeTimestamp32()
        {
            LocalDateTime ldt4 = new LocalDateTime(1971, 01, 01, 22, 45, 56);
            
            var a = MessagePackSerializer.Serialize(ldt4); // a = timestamp32 (byte[6])
            Assert.Equal(TestTools.Convert(ldt4), ldt4);
        }
    }

    [Collection("ResolverCollection")]
    public class TimestampTests2
    {
        [Fact]
        public void LocalDateTimestamp96_1()
        {
            LocalDate ld1 = new LocalDate(0001, 01, 01);
            
            var a = MessagePackSerializer.Serialize(ld1); // a = timestamp96 (byte[15])
            Assert.Equal(TestTools.Convert(ld1), ld1);
        }

        [Fact]
        public void LocalDateTimestamp96_2()
        {
            LocalDate ld2 = new LocalDate(9999, 01, 01);

            var a = MessagePackSerializer.Serialize(ld2); // a = timestamp96 (byte[15])
            Assert.Equal(TestTools.Convert(ld2), ld2);
        }

        [Fact]
        public void LocalDateTimestamp64()
        {
            LocalDate ld3 = new LocalDate(2108, 01, 01);

            var a = MessagePackSerializer.Serialize(ld3); // a = timestamp64 (byte[10])
            Assert.Equal(TestTools.Convert(ld3), ld3);
        }

        [Fact]
        public void LocalDateTimestamp32()
        {
            LocalDate ld4 = new LocalDate(1971, 01, 01);
            
            var a = MessagePackSerializer.Serialize(ld4); // a = timestamp32 (byte[6])
            Assert.Equal(TestTools.Convert(ld4), ld4);
        }
    }
    [Collection("ResolverCollection")]
    public class TimestampTests3
    {
        [Fact]
        public void InstantTimestamp96_1()
        {
            var dt = new DateTime(0001, 01, 01, 00, 00, 00, DateTimeKind.Utc);
            Instant inst1 = Instant.FromDateTimeUtc(dt);

            var a = MessagePackSerializer.Serialize(inst1); // a = timestamp96 (byte[15])
            Assert.Equal(TestTools.Convert(inst1), inst1);
        }

        [Fact]
        public void InstantTimestamp96_2()
        {
            var dt = new DateTime(9999, 01, 01, 00, 00, 00).ToUniversalTime();
            Instant inst2 = Instant.FromDateTimeUtc(dt);

            var a = MessagePackSerializer.Serialize(inst2); // a = timestamp96 (byte[15])
            Assert.Equal(TestTools.Convert(inst2), inst2);
        }

        [Fact]
        public void InstantTimestamp64()
        {
            var dt = new DateTime(2108, 01, 01, 00, 00, 00).ToUniversalTime();
            Instant inst3 = Instant.FromDateTimeUtc(dt);

            var a = MessagePackSerializer.Serialize(inst3); // a = timestamp64 (byte[10])
            Assert.Equal(TestTools.Convert(inst3), inst3);
        }

        [Fact]
        public void InstantTimestamp32()
        {
            var dt = new DateTime(1971, 01, 01, 00, 00, 00).ToUniversalTime();
            Instant inst3 = Instant.FromDateTimeUtc(dt);

            var a = MessagePackSerializer.Serialize(inst3); // a = timestamp32 (byte[6])
            Assert.Equal(TestTools.Convert(inst3), inst3);
        }
    }
}
