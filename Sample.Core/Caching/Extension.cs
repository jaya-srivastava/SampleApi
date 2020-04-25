using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Enyim.Caching;
using Enyim.Caching.Memcached;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Sample.Core.Caching.Caching
{
	static class JsonClientExtensions
	{
		public static readonly IContractResolver contractResolver = new EntityContractResolver();

		private static JsonSerializerSettings SettingsForShallowSerialization()
		{
			JsonSerializerSettings settings = new JsonSerializerSettings()
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
				PreserveReferencesHandling = PreserveReferencesHandling.Objects,
				MaxDepth = 1,	   
				NullValueHandling = NullValueHandling.Ignore,
				ContractResolver = contractResolver
			};
			return settings;
		}

		public static bool StoreJson<T>(this IMemcachedClient client, StoreMode storeMode, string key, T value) where T : class
		{
			
			var jsonString = JsonConvert.SerializeObject(value, Formatting.Indented /*, settings*/);  			
			return client.Store(storeMode, key, jsonString);
		}

		public static T GetJson<T>(this IMemcachedClient client, string key) where T : class
		{
			T obj = default(T);
			var jsonString = client.Get<string>(key);
           
			if (!string.IsNullOrWhiteSpace(jsonString))
				obj = JsonConvert.DeserializeObject<T>(jsonString);

			return obj;
		}
        public static string GetJsonString(this IMemcachedClient client, string key) 
        {
            var jsonString = client.Get<string>(key);
            return jsonString;
        }



	}

	class EntityContractResolver : DefaultContractResolver
	{
		protected override List<System.Reflection.MemberInfo> GetSerializableMembers(Type objectType)
		{
			if (objectType.Namespace.StartsWith("System.Data.Entity.Dynamic"))
			{
				return base.GetSerializableMembers(objectType.BaseType);
			}

			return base.GetSerializableMembers(objectType);
		}

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);
			property.ShouldSerialize = instance => instance.GetType().IsPrimitive || instance.GetType() == typeof(string) || instance.GetType() == typeof(decimal);
			return property;
		}
	}
}
