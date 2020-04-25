using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sample.Models.DTO
{
	public class JsonConv
	{

		public static string Serializer<T>(T t)
		{
			return JsonConvert.SerializeObject(t);
		}

		public static T DeSerializer<T>(string str)
		{
			T t = default(T);
            if (String.IsNullOrWhiteSpace(str)) return t;
			t = JsonConvert.DeserializeObject<T>(str);

			return t;
		}

	}

}
