using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiTraining.Core.Utility
{
    public class DynamicLockGenerator
    {
        private static Dictionary<string, object> LocksByKey = new Dictionary<string, object>();

        private static object Lock = new object();

        public static object GetLock(string key)
        {
            if (!LocksByKey.ContainsKey(key))
            {
                lock (Lock)
                {
                    if (!LocksByKey.ContainsKey(key))
                    {
                        LocksByKey.Add(key, new object());
                    }

                    return LocksByKey[key];
                }
            }
            else
            {
                return LocksByKey[key];
            }
        }
    }
}
