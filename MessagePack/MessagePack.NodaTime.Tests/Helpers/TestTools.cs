// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
namespace MessagePack.NodaTime.Tests.Helpers
{
    static class TestTools
    {
        public static T Convert<T>(T value)
        {
            return MessagePackSerializer.Deserialize<T>(MessagePackSerializer.Serialize(value));
        }

        public static T ConvertLZ4<T>(T value)
        {
            return LZ4MessagePackSerializer.Deserialize<T>(LZ4MessagePackSerializer.Serialize(value));
        }
    }
}
