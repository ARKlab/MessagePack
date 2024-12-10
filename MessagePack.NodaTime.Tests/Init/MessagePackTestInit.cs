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
            var resolver = CompositeResolver.Create(new[] {
                BuiltinResolver.Instance,
                AttributeFormatterResolver.Instance,
                SourceGeneratedFormatterResolver.Instance,
                NodatimeResolver.Instance,
                DynamicEnumAsStringResolver.Instance,
                ContractlessStandardResolver.Instance
            }
            );

            var options = MessagePackSerializerOptions.Standard.WithResolver(resolver);

            // pass options to every time or set as default
            MessagePackSerializer.DefaultOptions = options;
        }
    }
    [CollectionDefinition("ResolverCollection")]
    public class ResolverCollection : ICollectionFixture<ResolverFixture>
    {

    }
}
