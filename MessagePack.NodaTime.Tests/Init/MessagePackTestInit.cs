// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.Resolvers;
using Xunit;

namespace MessagePack.NodaTime.Tests.Utils
{
    public class ResolverFixture
    {
        public ResolverFixture()
        {
            CompositeResolver.RegisterAndSetAsDefault(
            BuiltinResolver.Instance,
            NodatimeResolver.Instance,
            AttributeFormatterResolver.Instance,
            DynamicEnumAsStringResolver.Instance,
            ContractlessStandardResolver.Instance
            );
        }
    }
    [CollectionDefinition("ResolverCollection")]
    public class ResolverCollection : ICollectionFixture<ResolverFixture>
    {

    }
}
