using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Core.Caching.Caching
{
    public interface ICacheProvider
    {
        void Add<T>(T entity, string key) where T : class;
        void Clear(string key);
        T Get<T>(string key) where T : class;

		int Count();
    }
}
