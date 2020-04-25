using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iPractice.Core;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using iPractice.Core.Caching;


namespace iPractice.Repository.Test
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
	class MemcacheTest
	{

		public static void MainTest()
		{
            //not required : ConfigTest();
			//GetSetMemoryTest();
            GetSetMemoryTestAsInWebApp();
		}

		public static void ConfigTest()
		{
			MemcachedClientConfiguration mc = new MemcachedClientConfiguration();

			mc.Protocol = MemcachedProtocol.Text;
			mc.AddServer(ConfigHelper.MemcacheHost1, ConfigHelper.MemcachePort1);
			//mc.AddServer(ConfigHelper.MemcacheHost2,ConfigHelper.MemcachePort2);
		}

        public static void GetSetMemoryTest()
		{
			MemCacheProvider cache = new MemCacheProvider();

			Person P =  new  Person { FirstName = "F1", LastName ="L1"};
			cache.Add<Person>(P, "Person");
			Object pGet = cache.Get<Person>("Person");

            bool result = MemCacheProvider.Remove("Person");

            //bool isPerson = MemCacheProvider.Exists("Person");

            //object pGet2 = MemCacheProvider.Get("Person");

            //MemCacheProvider.ClearAll();
			


		}


        public static void GetSetMemoryTestAsInWebApp()
        {
            MemCacheProvider cache = new MemCacheProvider();

            string keyPerson = "Person";
            Person P = new Person { FirstName = "F1", LastName = "L1" };
            cache.Add<Person>(P, keyPerson);
            string  str1 = cache.GetJsonString(keyPerson);

            Object pGet = cache.Get<Person>(keyPerson);

            bool result = MemCacheProvider.Remove(keyPerson);

            //bool isPerson = MemCacheProvider.Exists("Person");

            //object pGet2 = MemCacheProvider.Get("Person");

            //MemCacheProvider.ClearAll();



        }
	}
}
