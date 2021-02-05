using CoreWebApiTraining.Core.Utility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiTraining.Core.Caching.impl
{
    public class CachedObjects<TItem> : ICachedObjects<TItem>
    {
        private ConcurrentDictionary<string, CachedItem<TItem>> lstObj = new ConcurrentDictionary<string, CachedItem<TItem>>();

        public void Add(string key, TItem value, TimeSpan expiresAfter)
        {
            CachedItem<TItem> item = new CachedItem<TItem>(value, expiresAfter);
            this.lstObj.AddOrUpdate(key, item, (key, oldValue) => item);
        }

        public bool TryGet(string key, out TItem value)
        {
            if (this.lstObj.TryGetValue(key, out CachedItem<TItem> item))
            {
                if (DateTimeOffset.Now - item.Created <= item.ExpiresAfter)
                {
                    value = (TItem)item.Value;
                    return true;
                }
                else
                {
                    this.lstObj.TryRemove(key, out item);
                    value = default(TItem);
                    return false;
                }
            }
            else
            {
                value = default(TItem);
                return false;
            }
        }
    }

    public class CachedItem<T>
    {
        public T Value { get; set; }

        public DateTimeOffset Created { get; set; }

        public TimeSpan ExpiresAfter { get; set; }

        public CachedItem(T value, TimeSpan expiresAfter)
        {
            this.Created = DateTimeOffset.Now;
            this.Value = value;
            this.ExpiresAfter = expiresAfter;
        }

    }
}
