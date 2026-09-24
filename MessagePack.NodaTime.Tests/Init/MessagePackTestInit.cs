// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using MessagePack.Resolvers;

namespace MessagePack.NodaTime.Tests.Utils
{
    public static class MessagePackTestInit
    {
        [Before(Assembly)]
        public static void Initialize()
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
}
