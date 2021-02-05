using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiTraining.Core.Caching
{
    public interface ICachedObjectProvider<T>
    {
        bool Get(string key, out T result);

        void Set(string key, T value, TimeSpan expireAfter);
    }
}
