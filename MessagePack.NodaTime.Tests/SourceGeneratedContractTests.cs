// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack;
using MessagePack.Resolvers;
using NodaTime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MessagePack.NodaTime.Tests
{
    [MessagePackObject]
    public record MyClass
    {
        [Key(0)]
        public LocalDateTime LocalDateTime { get; set; } = LocalDateTime.FromDateTime(DateTime.Now);

        [Key(1)]
        public Instant Instant { get; set; } = Instant.FromDateTimeUtc(DateTime.UtcNow);

        [Key(2)]
        public Period Period { get; set; } = Period.Zero;

        [Key(3)]
        public LocalDate LocalDate { get; set; } = LocalDate.FromDateTime(DateTime.Now.Date);

        [Key(4)]
        public OffsetDateTime OffsetDateTime { get; set; } = OffsetDateTime.FromDateTimeOffset(DateTimeOffset.Now);

        [Key(5)]
        public Duration Duration { get; set; } = Duration.Zero;

        [Key(6)]
        public ZonedDateTime ZonedDateTime { get; set; } = ZonedDateTime.FromDateTimeOffset(DateTimeOffset.Now);
    }

    [Collection("ResolverCollection")]
    public class SourceGeneratedContractTests
    {
        [Fact]
        public void Roundtrip()
        {
            var o = new MyClass { LocalDateTime = LocalDateTime.FromDateTime(DateTime.Now) };
            var bin = MessagePackSerializer.Serialize(o);
            var res = MessagePackSerializer.Deserialize<MyClass>(bin);

            Assert.Equal(o.LocalDateTime, res.LocalDateTime); // in DateTime format due to 'abc' being DateTime object
        }
    }
}
