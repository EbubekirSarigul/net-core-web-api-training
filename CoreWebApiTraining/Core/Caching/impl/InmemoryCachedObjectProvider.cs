using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiTraining.Core.Caching.impl
{
    public class InmemoryCachedObjectProvider<T> : ICachedObjectProvider<T>
    {
        private readonly ICachedObjects<T> cachedObjects;

        public InmemoryCachedObjectProvider(ICachedObjects<T> cachedObjects)
        {
            this.cachedObjects = cachedObjects;
        }

        public bool Get(string key, out T result)
        {
            if (cachedObjects.TryGet(key, out result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Set(string key, T value, TimeSpan expireAfter)
        {
            cachedObjects.Add(key, value, expireAfter);
        }
    }
}
