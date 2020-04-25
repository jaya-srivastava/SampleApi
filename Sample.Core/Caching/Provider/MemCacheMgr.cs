using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using Enyim.Caching;
using System.Net;

namespace Sample.Core.Caching.Caching
{
	public static class MemCacheMgr
	{
		private readonly static IMemcachedClient _instance;
		private static Object thisLock = new Object();

        public readonly static IMemcachedClient OutputCacheInstance = new MemcachedClient(GetOutputCacheConfig());

        private static IMemcachedClientConfiguration GetOutputCacheConfig()
        {
            MemcachedClientConfiguration config = new MemcachedClientConfiguration();
            //http://stackoverflow.com/questions/4470758/enyim-memcached-client-does-not-write-read-data
            //also need to add	else cmd_get and cmd_set stats counters remain constant. The call to client.Get returns null.
            //config.Protocol = MemcachedProtocol.Text; to make the 

            //config.AddServer("127.0.0.1", 11211);
            if (!String.IsNullOrEmpty(ConfigHelper.MCacheOutputcacheHost1))
                config.AddServer(ConfigHelper.MCacheOutputcacheHost1, ConfigHelper.MCacheOutputcachePort1);
            else
                return null;
            //mc.AddServer(ConfigHelper.MemcacheHost2,ConfigHelper.MemcachePort2);
            config.Protocol = MemcachedProtocol.Text;
            return config;
        }

		static MemCacheMgr()
		{
			lock (thisLock)
			{
				_instance = CreateClient();//_instance = new MemcachedClient();
			}
		}

		public static IMemcachedClient Instance { get { return _instance; } }

		private static IMemcachedClientConfiguration GetConfig()
		{
			//MemcachedClientConfiguration config = new MemcachedClientConfiguration();
			////config.Servers.Add(new IPEndPoint(IPAddress.Loopback, 11211));
			//config.AddServer("127.0.0.1", 11211);
			//config.Protocol = MemcachedProtocol.Text;
			////config.Authentication.Type = typeof(PlainTextAuthenticator);
			////config.Authentication.Parameters["userName"] = "demo";
			////config.Authentication.Parameters["password"] = "demo";

			//var mc = new MemcachedClient(config);	 
			MemcachedClientConfiguration config = new MemcachedClientConfiguration();
			
			//http://stackoverflow.com/questions/4470758/enyim-memcached-client-does-not-write-read-data
			//also need to add	else cmd_get and cmd_set stats counters remain constant. The call to client.Get returns null.
			//config.Protocol = MemcachedProtocol.Text; to make the 

			//config.AddServer("127.0.0.1", 11211);
			config.AddServer(ConfigHelper.MemcacheHost1, ConfigHelper.MemcachePort1);
			//mc.AddServer(ConfigHelper.MemcacheHost2,ConfigHelper.MemcachePort2);
			config.Protocol = MemcachedProtocol.Text;
			//config.SocketPool.MinPoolSize = 10;
			//config.SocketPool.MaxPoolSize = 50;
			

			return config;	
		}

		private static IMemcachedClient CreateClient()
		{
			var config = GetConfig();
			MemcachedClient mc = new MemcachedClient(config);
			return mc;
		}


	}
}
