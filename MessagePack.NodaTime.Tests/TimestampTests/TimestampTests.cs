// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.NodaTime.Tests.Helpers;
using NodaTime;
using System;
using System.Threading.Tasks;

namespace MessagePack.NodaTime.Tests.TimestampTests
{
    public class TimestampTests1
    {
        [Test]
        public async Task LocalDateTimeTimestamp96_1()
        {
            LocalDateTime ldt1 = new LocalDateTime(0001, 01, 01, 00, 00, 00);

            var ts96_1 = MessagePackSerializer.Serialize(ldt1); // timestamp96 (byte[15])
            await Assert.That(ts96_1.Length).IsEqualTo(15);
        }

        [Test]
        public async Task LocalDateTimeTimestamp96_2()
        {
            LocalDateTime ldt2 = new LocalDateTime(9999, 01, 01, 00, 00, 00);

            var ts96_2 = MessagePackSerializer.Serialize(ldt2); // timestamp96 (byte[15])
            await Assert.That(ts96_2.Length).IsEqualTo(15);
        }

        [Test]
        public async Task LocalDateTimeTimestamp64()
        {
            LocalDateTime ldt3 = new LocalDateTime(2108, 01, 01, 00, 00, 00);

            var ts64 = MessagePackSerializer.Serialize(ldt3); // timestamp64 (byte[10])
            await Assert.That(ts64.Length).IsEqualTo(10);
        }

        [Test]
        public async Task LocalDateTimeTimestamp32()
        {
            LocalDateTime ldt4 = new LocalDateTime(1971, 01, 01, 22, 45, 56);

            var ts32 = MessagePackSerializer.Serialize(ldt4); // timestamp32 (byte[6])
            await Assert.That(ts32.Length).IsEqualTo(6);
        }
    }

    public class TimestampTests2
    {
        [Test]
        public async Task LocalDateTimestamp96_1()
        {
            LocalDate ld1 = new LocalDate(0001, 01, 01);

            var ts96_1 = MessagePackSerializer.Serialize(ld1); // timestamp96 (byte[15])
            await Assert.That(ts96_1.Length).IsEqualTo(15);
        }

        [Test]
        public async Task LocalDateTimestamp96_2()
        {
            LocalDate ld2 = new LocalDate(9999, 01, 01);

            var ts96_2 = MessagePackSerializer.Serialize(ld2); // timestamp96 (byte[15])
            await Assert.That(ts96_2.Length).IsEqualTo(15);
        }

        [Test]
        public async Task LocalDateTimestamp64()
        {
            LocalDate ld3 = new LocalDate(2108, 01, 01);

            var ts64 = MessagePackSerializer.Serialize(ld3); // timestamp64 (byte[10])
            await Assert.That(ts64.Length).IsEqualTo(10);
        }

        [Test]
        public async Task LocalDateTimestamp32()
        {
            LocalDate ld4 = new LocalDate(1971, 01, 01);

            var ts32 = MessagePackSerializer.Serialize(ld4); // timestamp32 (byte[6])
            await Assert.That(ts32.Length).IsEqualTo(6);
        }
    }

    public class TimestampTests3
    {
        [Test]
        public async Task InstantTimestamp96_1()
        {
            var dt = new DateTime(0001, 01, 01, 00, 00, 00).ToUniversalTime();
            Instant inst1 = Instant.FromDateTimeUtc(dt);

            var ts96_1 = MessagePackSerializer.Serialize(inst1); // timestamp96 (byte[15])
            await Assert.That(ts96_1.Length).IsEqualTo(15);
        }

        [Test]
        public async Task InstantTimestamp96_2()
        {
            var dt = new DateTime(9999, 01, 01, 00, 00, 00).ToUniversalTime();
            Instant inst2 = Instant.FromDateTimeUtc(dt);

            var ts96_2 = MessagePackSerializer.Serialize(inst2); // timestamp96 (byte[15])
            await Assert.That(ts96_2.Length).IsEqualTo(15);
        }

        [Test]
        public async Task InstantTimestamp64()
        {
            var dt = new DateTime(2108, 01, 01, 00, 00, 00).ToUniversalTime();
            Instant inst3 = Instant.FromDateTimeUtc(dt);

            var ts64 = MessagePackSerializer.Serialize(inst3); // timestamp64 (byte[10])
            await Assert.That(ts64.Length).IsEqualTo(10);
        }

        [Test]
        public async Task InstantTimestamp32()
        {
            var dt = new DateTime(1971, 01, 01, 00, 00, 00).ToUniversalTime();
            Instant inst4 = Instant.FromDateTimeUtc(dt);

            var ts32 = MessagePackSerializer.Serialize(inst4); // timestamp32 (byte[6])
            await Assert.That(ts32.Length).IsEqualTo(6);
        }
    }

    public class TimestampTests4
    {
        [Test]
        public async Task LocalDateTimestamp32WithNanos()
        {
            //only changes to timestamp64 if nanoseconds are 100 or more
            LocalDateTime ld1 = new LocalDateTime(1971, 01, 01, 00, 00, 00, 00).PlusNanoseconds(100);
            var ts32_64 = MessagePackSerializer.Serialize(ld1); // a = timestamp64 (byte[10])

            await Assert.That(ts32_64.Length).IsEqualTo(10);
        }

        [Test]
        public async Task LocalDateTimestamp32WithNanosTruncating()
        {
            //only changes to timestamp64 if nanoseconds are 100 or more
            LocalDateTime ld1 = new LocalDateTime(1971, 01, 01, 00, 00, 00, 00).PlusNanoseconds(99);

            var ts32_64 = MessagePackSerializer.Serialize(ld1); // a != timestamp64 (byte[10])
            await Assert.That(ts32_64.Length).IsEqualTo(6);
        }
    }
}
