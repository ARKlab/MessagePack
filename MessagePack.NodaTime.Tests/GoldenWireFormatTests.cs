// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information.
using NodaTime;
using System.Threading.Tasks;
using TUnit.Assertions.Enums;

namespace MessagePack.NodaTime.Tests
{
    public class GoldenWireFormatTests
    {
        [Test]
        public async Task Timestamp32()
        {
            byte[] bytes = [0xd6, 0xff, 0x00, 0x00, 0x00, 0x00];
            await Check(Instant.FromUnixTimeSeconds(0), bytes);
            await Check(new LocalDateTime(1970, 1, 1, 0, 0), bytes);
            await Check(new LocalDate(1970, 1, 1), bytes);
        }

        [Test]
        public async Task Timestamp64()
        {
            // 100 nanoseconds occupy the upper 30 bits; seconds occupy the lower 34.
            byte[] bytes = [0xd7, 0xff, 0x00, 0x00, 0x01, 0x90, 0x00, 0x00, 0x00, 0x00];
            await Check(Instant.FromUnixTimeTicks(1), bytes);
            await Check(new LocalDateTime(1970, 1, 1, 0, 0).PlusNanoseconds(100), bytes);
            await Check(new LocalDate(2106, 2, 8),
                [0xd7, 0xff, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0xf6, 0x80]);
        }

        [Test]
        public async Task Timestamp96()
        {
            byte[] bytes =
                [0xc7, 0x0c, 0xff, 0x00, 0x00, 0x00, 0x00, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff];
            await Check(Instant.FromUnixTimeSeconds(-1), bytes);
            await Check(new LocalDateTime(1969, 12, 31, 23, 59, 59), bytes);
            await Check(new LocalDate(1969, 12, 31),
                [0xc7, 0x0c, 0xff, 0x00, 0x00, 0x00, 0x00, 0xff, 0xff, 0xff, 0xff, 0xff, 0xfe, 0xae, 0x80]);
        }

        [Test]
        public async Task IntegerFormats()
        {
            await Check(Offset.FromSeconds(1), [0xd2, 0x00, 0x00, 0x00, 0x01]);
            await Check(Offset.FromSeconds(-1), [0xd2, 0xff, 0xff, 0xff, 0xff]);
            await Check(LocalTime.Midnight.PlusNanoseconds(1),
                [0xd3, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01]);
            // Duration uses compact BCL ticks, unlike the forced widths above.
            await Check(Duration.FromTicks(1), [0x01]);
            await Check(Duration.FromTicks(4294967296L),
                [0xcf, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00]);
            await Check(Duration.FromTicks(-4294967296L),
                [0xd3, 0xff, 0xff, 0xff, 0xff, 0x00, 0x00, 0x00, 0x00]);
        }

        [Test]
        public async Task CompoundArrays()
        {
            await Check(new LocalDateTime(1970, 1, 1, 0, 0).WithOffset(Offset.FromSeconds(1)),
                [0x92, 0xd6, 0xff, 0x00, 0x00, 0x00, 0x00, 0xd2, 0x00, 0x00, 0x00, 0x01]);
            await Check(Instant.FromUnixTimeSeconds(0).InUtc(),
                [0x93, 0xd6, 0xff, 0x00, 0x00, 0x00, 0x00, 0xd2, 0x00, 0x00, 0x00, 0x00,
                 0xa3, 0x55, 0x54, 0x43]);
        }

        [Test]
        public async Task PeriodArray()
        {
            var period = new PeriodBuilder
            {
                Years = 1, Months = 2, Weeks = 3, Days = 4, Hours = 5,
                Minutes = 6, Seconds = 7, Milliseconds = 8, Ticks = 9, Nanoseconds = 10
            }.Build();
            await Check(period,
                [0x9a,
                 0xd2, 0x00, 0x00, 0x00, 0x01,
                 0xd2, 0x00, 0x00, 0x00, 0x02,
                 0xd2, 0x00, 0x00, 0x00, 0x03,
                 0xd2, 0x00, 0x00, 0x00, 0x04,
                 0xd3, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05,
                 0xd3, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x06,
                 0xd3, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x07,
                 0xd3, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08,
                 0xd3, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x09,
                 0xd3, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0a]);
        }

        [Test]
        public async Task PeriodNull()
        {
            await Check<Period?>(null, [0xc0]);
        }

        [Test]
        public async Task NanosecondTruncation()
        {
            var epoch = new LocalDateTime(1970, 1, 1, 0, 0);
            byte[] timestamp = [0xd6, 0xff, 0x00, 0x00, 0x00, 0x00];
            await Check(epoch.PlusNanoseconds(99), timestamp, epoch);
            await Check(Instant.FromUnixTimeSeconds(0) + Duration.FromNanoseconds(99),
                timestamp, Instant.FromUnixTimeSeconds(0));
            await Check(Duration.FromNanoseconds(199), [0x01], Duration.FromTicks(1));
            await Check(Duration.FromNanoseconds(-199), [0xff], Duration.FromTicks(-1));
        }

        private static Task Check<T>(T value, byte[] bytes) => Check(value, bytes, value);

        private static async Task Check<T>(T value, byte[] bytes, T decoded)
        {
            await Assert.That(MessagePackSerializer.Serialize(value))
                .IsEquivalentTo(bytes, CollectionOrdering.Matching);
            await Assert.That(MessagePackSerializer.Deserialize<T>(bytes)).IsEqualTo(decoded);
        }
    }
}
