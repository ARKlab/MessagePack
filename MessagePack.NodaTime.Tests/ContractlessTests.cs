// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.Resolvers;
using NodaTime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagePack.NodaTime.Tests
{
    public class LDT
    {
        public LocalDateTime ldt { get; set; }
    }

    public class ContractlessTests
    {
        [Test]
        public async Task AnonType()
        {
            var o = new { Ldt = LocalDateTime.FromDateTime(DateTime.Now) };
            var bin = MessagePackSerializer.Serialize(o);
            var res = MessagePackSerializer.Deserialize<object>(bin);

            var abc = ((IEnumerable)res).Cast<KeyValuePair<object, object>>().First().Value;

            await Assert.That(abc).IsEqualTo(o.Ldt.ToDateTimeUnspecified()); // in DateTime format due to 'abc' being DateTime object
        }

        [Test]
        public async Task AnonTypeWithClassProperty()
        {
            var o = new { ldt = LocalDateTime.FromDateTime(DateTime.Now) };
            var bin = MessagePackSerializer.Serialize(o);
            var res = MessagePackSerializer.Deserialize<LDT>(bin);

            await Assert.That(res.ldt).IsEqualTo(o.ldt);
        }

        [Test]
        [Skip("object cannot be serialized due to DateTime part of Nodatime type")]
        public async Task ObjectToDynamic()
        {
            object o = new ZonedDateTime();
            var bin = MessagePackSerializer.Serialize(o);
            var res = MessagePackSerializer.Deserialize<dynamic>(bin);

            await Assert.That((object)res).IsEqualTo(o);
        }

        [Test]
        public async Task ObjectToLDT()
        {
            object o = new LocalDateTime();
            var bin = MessagePackSerializer.Serialize(o);
            var res = MessagePackSerializer.Deserialize<LocalDateTime>(bin);

            await Assert.That((object)res).IsEqualTo(o);
        }

        [Test]
        public async Task ObjectToInstant()
        {
            object o = new Instant();
            var bin = MessagePackSerializer.Serialize(o);
            var res = MessagePackSerializer.Deserialize<Instant>(bin);

            await Assert.That((object)res).IsEqualTo(o);
        }

        [Test]
        public async Task DynamicToLDT()
        {
            dynamic d = new LocalDate();
            var bin = MessagePackSerializer.Serialize(d);
            var res = MessagePackSerializer.Deserialize<LocalDate>(bin);
            await Assert.That((object)res).IsEqualTo((object)d);
        }

        [Test]
        [Skip("cannot be deserialized as dynamic")]
        public async Task ZonedDTToDynamic()
        {
            var d = new ZonedDateTime();
            var bin = MessagePackSerializer.Serialize(d);
            var res = MessagePackSerializer.Deserialize<dynamic>(bin);

            await Assert.That((object)res).IsEqualTo(d);
        }

        [Test]
        public async Task ObjectWithNonGeneric()
        {
            object o = new LocalDateTime();
            var bin = MessagePackSerializer.Serialize(o.GetType(), o);
            var res = MessagePackSerializer.Deserialize(o.GetType(), bin);

            await Assert.That(res).IsEqualTo(o);
        }
    }
}
