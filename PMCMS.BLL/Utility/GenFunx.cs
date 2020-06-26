using System;

namespace PMCMS.BLL.Utility
{
    public static class GenFunx
    {
        /// <summary>
        /// Converts enum to int value
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static int ToInt(this Enum enumValue)
        {
            return Convert.ToInt32(enumValue);
        }

        /// <summary>
        /// Converts string to int, returns 0 if can't convert.
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static int ToInt(this string stringValue)
        {
            int val = 0;
            try
            {
                val = Convert.ToInt32(stringValue);
            }
            catch (Exception)
            {
                //returns 0 if cannot convert
            }
            return val;
        }

        /// <summary>
        /// Clears the text from whitespace, dots, semi-colons, ampersants, asterikses and slashes
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ClearText(this string text)
        {
            return text.Trim(' ', '.', ';', '&', '*', '/', '\\');
        }

        public static bool IsUsable(this string text)
        {
            return (!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text) && !text.Contains(".") && !text.Contains(";") && !text.Contains("&") && !text.Contains("*") && !text.Contains("/") && !text.Contains("\\"));
        }
    }
}
