// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using System;
using System.Threading.Tasks;

namespace MessagePack.NodaTime.Tests.Helpers
{
    static class TestTools
    {
        public static T Convert<T>(T value)
        {
            return MessagePackSerializer.Deserialize<T>(MessagePackSerializer.Serialize(value));
        }

        public static async Task ThrowsInner<T>(Func<object> testCode) where T : Exception
        {
            bool found = false;
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
                        found = true;
                        break;
                    }

                    currex = currex.InnerException;
                }
            }

            await Assert.That(found).IsTrue().Because($"Exception of type {typeof(T).Name} must be thrown");

        }
    }
}
