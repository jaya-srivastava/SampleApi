using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sample.Core.Caching
{
    public static class StringExtenstions
    {
        public static string ToStringURLBuilder(this string stringExp)
        {   
            stringExp = stringExp.Replace(":", "-");
            stringExp = stringExp.Trim().TrimEnd('-');
            stringExp = stringExp.Replace("&", "-");
            stringExp = stringExp.Replace(",", "");
            stringExp = stringExp.Replace("/", "-");
            stringExp = stringExp.Replace("(", "-").Replace(")", "-");
            stringExp = stringExp.Replace(":", "-");
            stringExp = stringExp.Replace(" ", "-");
            stringExp = stringExp.Replace("----", "-").Replace("---", "-").Replace("--", "-");
            //stringExp = Regex.Replace(stringExp, @"\s+", "-");
            return stringExp;
        }

        public static string ToLower_StringURLBuilder(this string stringExp)
        {
            return stringExp.ToStringURLBuilder().ToLower();
        }
        public static string ToLower_StringURLBuilderForWorksheet(this string stringExp)
        {
		    stringExp = stringExp.Replace(":", "-");
            stringExp = stringExp.Trim().TrimEnd('-');
            stringExp = stringExp.Replace("(", "-").Replace(")", "-");
 			stringExp = stringExp.Replace(",", "");            
            stringExp = stringExp.Replace(" ", "-");
            stringExp = stringExp.Replace(":", "-");
            stringExp = stringExp.Replace("----", "-").Replace("---", "-").Replace("--", "-");
            //stringExp = Regex.Replace(stringExp, @"\s+", "-");
			//stringExp = stringExp.Replace("&", "-");
            return stringExp.ToLower();
        }
    }
}
