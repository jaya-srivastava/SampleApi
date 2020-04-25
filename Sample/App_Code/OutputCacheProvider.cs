using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using Enyim.Caching;
using Enyim.Caching.Memcached;
using Enyim.Caching.Configuration;
using Sample.Core.Caching;


namespace iPractice.Cache
{
    public class MemcacheOutputCacheProvider : OutputCacheProvider
    {
        private static IMemcachedClient client = MemCacheMgr.OutputCacheInstance;

        public override object Add(string key, object entry, DateTime utcExpiry)
        {
           
            if (client.Get(key) != null)
                return client.Get(key);
            else
            {
                //var objString = entry.ToString();
                bool result = client.Store(StoreMode.Set, key, entry);
                return result.ToString();
            }
        }

        public override object Get(string key)
        {
            object obj = client.Get(key);
            //if(obj ==null)
            //    client.Store(StoreMode.Add, key, entry);
            return obj;
        }

        public override void Remove(string key)
        {
            client.Remove(key);

        }

        public override void Set(string key, object entry, DateTime utcExpiry)
        {
            utcExpiry = TimeZoneInfo.ConvertTimeFromUtc(utcExpiry, TimeZoneInfo.Local);
            bool result = client.Store(StoreMode.Set, key, entry);
            //return result;
        }
    }
}