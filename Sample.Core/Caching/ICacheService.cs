using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Core.Caching.Caching
{
    interface ICacheService
    {
        T Get<T>(string cacheID, Func<T> func) where T : class;

        TResult Get<TArg, TResult>(string cacheID, TArg args, Func<TArg, TResult> func) where TResult : class;

        TResult Get<TArg1, TArg2, TResult>(string cacheID, TArg1 arg1, TArg2 arg2, Func<TArg1, TArg2, TResult> func) where TResult : class;
    
		//TResult Get<TArg1, TArg2, TArg3, TResult>(string cacheID, TArg1 arg1, TArg2 arg2, TArg3 arg3, Func<TArg1, TArg2, TArg3, TResult> func) where TResult : class;

		//TResult Get<TArg1, TArg2, TArg3, TArg4, TResult>(string cacheID, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Func<TArg1, TArg2, TArg3, TArg4, TResult> func) where TResult : class;
        
     }
}
