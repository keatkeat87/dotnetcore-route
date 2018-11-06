using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.Module.Stooges
{
    public static class StringExtensions
    {
        public static string ClearSpace(this string value)
        {
            string pattern = @"\s+";
            string replacement = "";
            Regex rgx = new Regex(pattern);         
            return rgx.Replace(value, replacement);
        }

        public static string FirstCharToUpper(this string value)
        {
            string endValue = (value.Length <= 1) ? "" : value.Substring(1);
            return char.ToUpper(value[0]) + endValue;
        }

        public static string TitleCase(this string value)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(value);
        }

        public static string CamelCaseToRegularString(this string value)
        {
            return Regex.Replace(value, "(\\B[A-Z])", " $1").ToLower();
        }

        public static T GetValueByKey<T>(this object obj, string key)
        {
            return (T)obj.GetType().GetProperty(key).GetValue(obj, null);
        }
    }
}
