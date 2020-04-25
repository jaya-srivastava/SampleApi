using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Repository.Helpers
{
    public static class CommonHelper
    {
        public static DateTime GetUTCTime()
        {
            return DateTime.Now.ToUniversalTime();
        }
    }
}
