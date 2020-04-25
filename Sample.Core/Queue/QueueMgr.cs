using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using Newtonsoft.Json;

namespace Sample.Core.Caching
{
    public static class QueueMgr
    {
        private static QueueProvider queue = new QueueProvider();
        public static T Get<T>(string queueName)
        {
            T t = queue.Get<T>(queueName);
            return t;
        }

        public static string GetString(string queueName)
        {
            string result = queue.GetString(queueName);
            return result;
        }

        public static bool Set<T>(string queueName, T val)
        {
            bool result = queue.Set<T>(queueName,val);
            return result;
        }
             
    }
}
