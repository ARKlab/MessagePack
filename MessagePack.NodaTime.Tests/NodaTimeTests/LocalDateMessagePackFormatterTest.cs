// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.NodaTime.Tests.Helpers;
using NodaTime;
using System;
using System.Threading.Tasks;
using TUnit.Assertions.Enums;

namespace MessagePack.NodaTime.Tests
{
    public class LocalDateMessagePackFormatterTest
    {   
        [Test]
        public async Task LocalDateTest()
        {
            LocalDate ld = LocalDate.FromDateTime(DateTime.Now);      
            await Assert.That(TestTools.Convert(ld)).IsEqualTo(ld);
        }

        [Test]
        public async Task NullableLocalDateTest()
        {
            LocalDate? ld = null;
            await Assert.That(TestTools.Convert(ld)).IsEqualTo(ld);
        }
                
        [Test]
        public async Task LocalDateArrayTest()
        {
            LocalDate[] ld =
                { LocalDate.FromDateTime(DateTime.Now),
                LocalDate.FromDateTime(new DateTime()),
                LocalDate.FromDateTime(DateTime.Now.AddTicks(500)),
                LocalDate.FromDateTime(DateTime.Now.AddMonths(10)),
                LocalDate.FromDateTime(new DateTime(2010,10,10))
            };
            await Assert.That(TestTools.Convert(ld)).IsEquivalentTo(ld, CollectionOrdering.Matching);
        }

        [Test]
        public async Task NullableLocalDateArrayTest()
        {
            LocalDate?[] ld = new LocalDate?[] {
                null,
                null,
                null,
                null,
                null
            };
            await Assert.That(TestTools.Convert(ld)).IsEquivalentTo(ld, CollectionOrdering.Matching);
        }        
    }
}
