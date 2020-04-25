using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sample.Core.Caching.Caching
{
    public static class CacheServiceHelper
    {
        private static readonly ICacheService cacheService =new MemCacheService();
		private static readonly string IsCacheEnabled = ConfigurationManager.AppSettings["IsCacheEnabled"] ?? String.Empty;	

        public static T Get<T>(string cacheID, Func<T> func) where T : class
        {
			return  IsCacheEnabled.ToLowerInvariant() =="true" ? cacheService.Get<T>(cacheID, func) : func();
        }

        public static T Get<Tin, T>(string cacheID, Tin args, Func<Tin,T> func) where T :class
        {
            return IsCacheEnabled.ToLowerInvariant() =="true" ? cacheService.Get<Tin,T>(cacheID, args, func) : func(args);
        }

		//public static TResult Get<TArg1, TArg2, TResult>(string cacheID, TArg1 arg1, TArg2 arg2, Func<TArg1, TArg2, TResult> func) where TResult : class
		//{
		//	return IsCacheEnabled.ToLowerInvariant() =="true" ? cacheService.Get<TArg1, TArg2, TResult>(cacheID, arg1, arg2, func) : func(arg1, arg2);
		//}

		//public static TResult Get<TArg1, TArg2, TArg3, TResult>(string cacheID, TArg1 arg1, TArg2 arg2, TArg3 arg3, Func<TArg1, TArg2, TArg3, TResult> func) where TResult : class
		//{
		//	return cacheService.Get<TArg1, TArg2, TArg3, TResult>(cacheID, arg1, arg2, arg3, func);
		//}

		//public static TResult Get<TArg1, TArg2, TArg3, TArg4, TResult>(string cacheID, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Func<TArg1, TArg2, TArg3, TArg4, TResult> func) where TResult : class
		//{
		//	return cacheService.Get<TArg1, TArg2, TArg3, TArg4, TResult>(cacheID, arg1, arg2, arg3, arg4, func);
		//}
 


    }
}
