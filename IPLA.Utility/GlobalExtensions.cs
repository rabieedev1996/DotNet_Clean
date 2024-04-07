using MD.PersianDateTime;
using MD.PersianDateTime.Standard;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Clean.Utility
{
    public static class GlobalExtensions
    {
        public static string RemoveRegular(this string text, string regularExpression)
        {
            text = Regex.Replace(text, regularExpression, " ").Trim().ToString();
            return text;
        }
        public static string MySubstring(this string text, int charachterCount)
        {
            if (charachterCount >= text.Length)
            {

                return text.Substring(0, text.Length);
            }
            return text.Substring(0, charachterCount);
        }
        public static string GetEnumDescription<T>(this T value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return "نامشخص";
            }
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Any() ? ((DescriptionAttribute)attributes.ElementAt(0)).Description
                        : "";
        }
        public static string ToFa(this DateTime date, string format)
        {
            var persianDateTime = new PersianDateTime(date);
            return persianDateTime.ToString(format);    
        }
        public static DateTime ToEn(this string persianDate)
        {
            var datetime =  PersianDateTime.Parse(persianDate);
            return datetime.ToDateTime();
        }
    }
}