using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using Enyim.Caching;
using Enyim.Caching.Memcached;


namespace Sample.Core.Caching.Caching
{
    public class MemCacheProvider : ICacheProvider
    {
		private static IMemcachedClient Cache = MemCacheMgr.Instance;
		private const Double cacheDuration = 60;
        //private static MemoryCache Cache = MemoryCache.Default;

        public void Add<T>(T entity, string key) where T : class
        {
           // bool stroeResult = Cache.Store(StoreMode.Set, key, entity, DateTime.Now.AddMinutes(cacheDuration));
			bool stroeResult = Cache.StoreJson<T>(StoreMode.Set, key, entity);
	    }

        //using object rather than type safe T
        public static void Add(object objectToCache, string key, int cacheDuration)
        {
            Cache.Store(StoreMode.Set, key, objectToCache, DateTime.Now.AddMinutes(cacheDuration));
        }

        //using object rather than type safe T
        public static void Add(object objectToCache, string key)
        {
            Cache.Store(StoreMode.Set, key, objectToCache);
        }

		//using object rather than type safe T
		public static void Set(object objectToCache, string key, int cacheDuration)
		{
			Cache.Store(StoreMode.Set, key, objectToCache, DateTime.Now.AddMinutes(cacheDuration));
		}

		public static string GetJson(string key)
		{
			return Cache.GetJsonString(key);			
		}

        //public static string GetJsonString(string key)
        //{
        //    return Cache.Get<string>(key);
        //}

		public int Count()
		{
			//var serverStats = Cache.Stats();
			//serverStats.GetValue                                                                                              
			//serverStats.
			//Console.WriteLine(count);
			return -1;
		}


        public static bool Remove(string key)
        {
            return Cache.Remove(key);
        }


        /// <summary>
        /// Clears all stored objects from memory.
        /// </summary>
        public static void ClearAll()
        {
			Cache.FlushAll();
			
        }

        /// <summary>
        /// Check for item in cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        /// <returns>A boolean if the object exists</returns>
        public static bool Exists(string key)
        {
            return Cache.Get(key) != null;
        }		

        public void Clear(string key)
        {
            Cache.Remove(key);
            //throw new NotImplementedException();
        }

        public T Get<T>(string key) where T : class
        {
            try
            {
                //return Cache.Get<T>(key);
				return Cache.GetJson<T>(key);
            }
            catch
            {
				System.Diagnostics.Debug.WriteLine("missed Get for item {0})", key);
                return null;
            }

        }

        public string GetJsonString(string key)
        {
            try
            {
                //return Cache.Get<T>(key);
                return Cache.GetJsonString(key);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("missed Get for item {0})", key);
                return null;
            }

        }

	}
}
