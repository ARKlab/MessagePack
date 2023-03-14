// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.Resolvers;
using NodaTime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MessagePack.NodaTime.Tests
{
    public class LDT
    {
        public LocalDateTime ldt { get; set; }
    }

    [Collection("ResolverCollection")]
    public class ObjectTesting
    {
        [Fact]
        public void AnonType()
        {
            var o = new { Ldt = LocalDateTime.FromDateTime(DateTime.Now) };
            var bin = MessagePackSerializer.Serialize(o);
            var res = MessagePackSerializer.Deserialize<object>(bin);

            var abc = ((IEnumerable)res).Cast<KeyValuePair<object, object>>().First().Value;

            Assert.Equal(o.Ldt.ToDateTimeUnspecified(), abc); // in DateTime format due to 'abc' being DateTime object
        }

        [Fact]
        public void AnonTypeWithClassProperty()
        {
            var o = new { ldt = LocalDateTime.FromDateTime(DateTime.Now) };
            var bin = MessagePackSerializer.Serialize(o);
            var res = MessagePackSerializer.Deserialize<LDT>(bin);

            Assert.Equal(o.ldt, res.ldt);
        }

        [Fact(Skip = "object cannot be serialized due to DateTime part of Nodatime type")]
        public void ObjectToDynamic()
        {
            object o = new ZonedDateTime();
            var bin = MessagePackSerializer.Serialize(o);
            var res = MessagePackSerializer.Deserialize<dynamic>(bin);

            Assert.Equal(o, res);
        }

        [Fact]
        public void ObjectToLDT()
        {
            object o = new LocalDateTime();
            var bin = MessagePackSerializer.Serialize(o);
            var res = MessagePackSerializer.Deserialize<LocalDateTime>(bin);

            Assert.Equal(o, res);
        }

        [Fact]
        public void ObjectToInstant()
        {
            object o = new Instant();
            var bin = MessagePackSerializer.Serialize(o);
            var res = MessagePackSerializer.Deserialize<Instant>(bin);

            Assert.Equal(o, res);
        }

        [Fact]
        public void DynamicToLDT()
        {
            dynamic d = new LocalDate();
            var bin = MessagePackSerializer.Serialize(d);
            var res = MessagePackSerializer.Deserialize<LocalDate>(bin);
            Assert.Equal(d, res);
        }

        [Fact(Skip = "cannot be deserialized as dynamic")]
        public void ZonedDTToDynamic()
        {
            var d = new ZonedDateTime();
            var bin = MessagePackSerializer.Serialize(d);
            var res = MessagePackSerializer.Deserialize<dynamic>(bin);

            Assert.Equal(d, res);
        }

        [Fact]
        public void ObjectWithNonGeneric()
        {
            object o = new LocalDateTime();
            var bin = MessagePackSerializer.Serialize(o.GetType(), o);
            var res = MessagePackSerializer.Deserialize(o.GetType(), bin);

            Assert.Equal(o, res);
        }
    }
}
