using System;

namespace Screentek.EPi.Imageshop.Extensions
{
    public static class StringExtensions
    {
        public static string UrlEncode(this string input)
        {
            return Uri.EscapeDataString(input);
        }

        public static string UrlDecode(this string input)
        {
            return Uri.UnescapeDataString(input);
        }
    }
}
