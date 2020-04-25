using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;



namespace Sample.Core.Caching.Caching
{
    class InMemoryCacheProvider : ICacheProvider
    {
        static MemoryCache _cache = MemoryCache.Default;
        private const Double ChacheExpirationInMinutes = 10;

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="entity">item cached</param>
        /// <param name="key">Name of item</param>
        public  void Add<T>(T entity, string key) where T : class
        {
			if (entity == null) return;

            if (_cache == null)
            {
                _cache = MemoryCache.Default;
            }
            if (_cache.Contains(key))
                _cache.Remove(key);

            var cacheItemPolicy = GetCacheItemPlicy();
            _cache.Set(key, entity, cacheItemPolicy);

        }

		public int Count()
		{
			var count = MemoryCache.Default.GetCount();
			//Console.WriteLine(count);
			return (int)count;
		}

        /// <summary>
        /// Remove item from cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        public void Clear(string key)
        {
            if (_cache == null)
            {
                _cache = MemoryCache.Default;
                return;
            }
            _cache.Remove(key);
        }

        /// <summary>
        /// Retrieve cached item
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of cached item</param>
        /// <returns>Cached item as type</returns>
        public  T Get<T>(string key) where T : class
        {
            if (_cache == null)
            {
                _cache = MemoryCache.Default;
            }
            try
            {
                return (T)_cache.Get(key);
            }
            catch
            {
                return null;
            }
        }


        public static bool UseCacheItemPolicy()
        {
            return false;
        }

        public CacheItemPolicy GetCacheItemPlicy()
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMinutes(ChacheExpirationInMinutes);
            return cacheItemPolicy;
        }


        //public static void Add<T>(T entity, string key) where T : class
        //{
        //    throw new NotImplementedException();
        //}

        //public static void Clear(string key)
        //{
        //    throw new NotImplementedException();
        //}

        //public static T Get<T>(string key) where T : class
        //{
        //    throw new NotImplementedException();
        //}
    }
}
