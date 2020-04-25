using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPractice.Helpers
{
    public static class StringExtensions
    {
        public static string Truncate(this string s, int maxLength)
        {
            if (string.IsNullOrEmpty(s) || maxLength <= 0)
                return string.Empty;
            else if (s.Length > maxLength)
                return s.Substring(0, maxLength) + "...";
            else
                return s;
        }

        // IsNumeric Function
        public static bool IsNumeric(this string Expression)
        {
            // Variable to collect the Return value of the TryParse method.
            bool isNum;

            // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
            double retNum;

            // The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
            // The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
            isNum = Double.TryParse(Expression, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        public static string RemoveLineBreaks(this string lines)
        {
            return lines.Replace("\r", "").Replace("\n", "");
        }

        public static string ReplaceLineBreaks(this string lines, string replacement)
        {
            return lines.Replace("\r\n", replacement)
                        .Replace("\r", replacement)
                        .Replace("\n", replacement);
        }

		public static T RandomNum<T>(this List<T> list, int maxForRandom)
		{
			if (list == null || list.Count == 0) return default(T);
			Random r = new Random();
			maxForRandom = list.Count > maxForRandom ? maxForRandom : list.Count;
			int i = r.Next(maxForRandom); 
			return list[i];	
		}

    }
}