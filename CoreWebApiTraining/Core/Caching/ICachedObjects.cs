using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiTraining.Core.Caching
{
    public interface ICachedObjects<TItem>
    {
        void Add(string key, TItem value, TimeSpan expiresAfter);

        bool TryGet(string key, out TItem value);
    }
}
