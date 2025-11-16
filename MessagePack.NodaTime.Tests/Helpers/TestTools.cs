// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using System;
using Xunit;

namespace MessagePack.NodaTime.Tests.Helpers
{
    static class TestTools
    {
        public static T Convert<T>(T value)
        {
            return MessagePackSerializer.Deserialize<T>(MessagePackSerializer.Serialize(value));
        }

        public static void ThrowsInner<T>(Func<object> testCode) where T : Exception
        {
            try
            {
                testCode.Invoke();
            }
            catch(Exception ex)
            {
                var currex = ex;

                while (currex != null)
                {
                    if (currex is T)
                    {
                        return;
                    }

                    currex = currex.InnerException;
                }
            }

            Assert.Fail($"Extention of type {typeof(T).Name} is not throwed");

        }
    }
}
