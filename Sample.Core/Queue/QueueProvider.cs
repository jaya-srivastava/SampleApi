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
    class QueueProvider
    {
        private static MemcachedClient client = new MemcachedClient(GetConfig());

        public T Get<T>(string queueName)
        {
            string key = String.Format("{0}/close/open", queueName);
            T t = default(T);
            string result = client.Get<string>(key);
            if (result != null)
                t = JsonConvert.DeserializeObject<T>(result);
            return t;
        }

        public string GetString(string queueName)
        {
            string key = String.Format("{0}/close/open", queueName);
            string result = client.Get<string>(key);
            return result;
        }

        public bool Set<T>(string queueName, T val)
        {
            bool result = false;
            string jsonString = JsonConvert.SerializeObject(val);
            result = client.Store(StoreMode.Set, queueName, jsonString);
            return result;
        }

        static IMemcachedClientConfiguration GetConfig()
        {
            MemcachedClientConfiguration config = new MemcachedClientConfiguration();
            ServerInfo serverInfo = ConfigHelper.QueueServerInfo1;

            //config.AddServer("127.0.0.1", 22133);
            config.AddServer(serverInfo.Host, serverInfo.Port);
            config.Protocol = MemcachedProtocol.Text;
            config.SocketPool.MinPoolSize = 10;
            config.SocketPool.MaxPoolSize = 100;
            return config;
        }

    }
}
