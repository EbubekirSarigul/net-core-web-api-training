using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiTraining.AppSettings
{
    public class EagerCacheSettings
    {
        public int minutesToCache { get; set; }
        public int refreshInterval { get; set; }

    }
}
