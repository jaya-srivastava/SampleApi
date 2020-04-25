using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using System.Configuration;

namespace Sample.Core.Caching.Caching
{
    class MemCacheService : ICacheService
    {
        static ICacheProvider cacheProvider = null; //new InMemoryCacheProvider();

		static MemCacheService()
		{
			
			bool IsMemCacheEnabled = ConfigHelper.IsMemCacheEnabled; // ConfigurationManager.AppSettings["IsMemCacheEnabled"]; 

			if (IsMemCacheEnabled) cacheProvider = new MemCacheProvider(); else cacheProvider = new InMemoryCacheProvider();
		}

        public T Get<T>(string cacheID, Func<T> func) where T : class
        {
            T item = cacheProvider.Get<T>(cacheID);
            if (item == null)
            {
                item = func();
				if (item != null)
					cacheProvider.Add<T>(item, cacheID);
            }
            return item;
        }

        public TResult Get<TArg, TResult>(string cacheID, TArg args, Func<TArg, TResult> func) where TResult : class
        {
            TResult item = cacheProvider.Get<TResult>(cacheID);
            if (item == null)
            {
                item = func.Invoke(args);
                if(item !=null)
                    cacheProvider.Add<TResult>(item, cacheID);
            }
            return item;
        }  

        public TResult Get<TArg1, TArg2, TResult>(string cacheID, TArg1 arg1, TArg2 arg2, Func<TArg1, TArg2, TResult> func) where TResult : class
        {
            TResult item = cacheProvider.Get<TResult>(cacheID);
            if (item == null)
            {
                item = func.Invoke(arg1, arg2);
                if (item != null)
                  cacheProvider.Add<TResult>(item, cacheID);
            }
            return item;
        }

		//public TResult Get<TArg1, TArg2, TArg3, TResult>(string cacheID, TArg1 arg1, TArg2 arg2, TArg3 arg3, Func<TArg1, TArg2, TArg3, TResult> func) where TResult : class
		//{
		//	TResult item = cacheProvider.Get<TResult>(cacheID);
		//	if (item == null)
		//	{
		//		item = func.Invoke(arg1, arg2, arg3);
		//		if (item != null)
		//			cacheProvider.Add<TResult>(item, cacheID);
		//	}
		//	return item;
		//}

		//public TResult Get<TArg1, TArg2, TArg3, TArg4, TResult>(string cacheID, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Func<TArg1, TArg2, TArg3, TArg4, TResult> func) where TResult : class
		//{
		//	TResult item = cacheProvider.Get<TResult>(cacheID);
		//	if (item == null)
		//	{
		//		item = func.Invoke(arg1, arg2, arg3, arg4);
		//		if (item != null)
		//			cacheProvider.Add<TResult>(item, cacheID);
		//	}
		//	return item;
		//}
    }


}
