using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace iPractice.RestServices.Helper
{
    public static class CommonHelper
    {
        public static string BaseUrl = ConfigurationManager.AppSettings["BaseUrl"].ToString();
    }
}
