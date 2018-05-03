// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.NodaTime.Tests.Helpers;
using NodaTime;
using System;
using Xunit;

namespace MessagePack.NodaTime.Tests
{
    public class NodatimeResolverTest
    {
        /********** NOT COMPLETE ************/

        // **********************************
        //Serializing and de-serializing
        T Convert<T>(T value)
        {
            return MessagePackSerializer.Deserialize<T>(MessagePackSerializer.Serialize(value, NodatimeResolver.Instance), NodatimeResolver.Instance);
        }

        public static object[] data = new object[]
        {
            Instant.FromDateTimeUtc(DateTime.UtcNow),
            LocalDate.FromDateTime(DateTime.Now),
            LocalDateTime.FromDateTime(DateTime.Now),
            LocalTime.FromSecondsSinceMidnight(1),
            Offset.FromHours(1),
            new OffsetDateTime(LocalDateTime.FromDateTime(DateTime.Now), new Offset()),
            Period.FromDays(1),
            new ZonedDateTime(Instant.FromDateTimeUtc(DateTime.UtcNow), DateTimeZone.Utc)
        };

        //[Theory]
        //[InlineData(Instant.MaxValue)]
        //[InlineData(typeof(LocalDate))]
        //[InlineData(typeof(LocalDateTime))]
        //[InlineData(typeof(LocalTime))]
        //[InlineData(typeof(OffsetDateTime))]
        //[InlineData(typeof(Offset))]
        //[InlineData(typeof(Period))]
        //[InlineData(typeof(ZonedDateTime))]
        //[MemberData(nameof(data))]
        public void NodaTimeResolverTest<T>(T value)
        {            
            Assert.Equal(TestTools.Convert(value), value);
        }
    }
}
