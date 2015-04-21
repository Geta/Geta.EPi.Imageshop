using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Geta.EPi.Imageshop.Extensions
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

        public static string Link(this string s, string url)
        {
            return string.Format(" <a href=\"{0}\" target=\"_blank\">{1}</a>", url, s);
        }

        public static string ParseURL(this string s)
        {
            return Regex.Replace(s, @"http(s)?://([\w-]+\.)+[\w-]+(/\S\w[\w- ;,./?%&=]\S*)?", URL);
        }

        public static string ParseTwitterEmoji(this string input)
        {
            const string format = "<img src=\"https://abs.twimg.com/emoji/v1/72x72/{0}.png\">";

            return ParseEmojiCharacters(input, format);
        }

        private static string ParseEmojiCharacters(string input, string format)
        {
            var builder = new StringBuilder();
            var enumerator = StringInfo.GetTextElementEnumerator(input);
            while (enumerator.MoveNext())
            {
                var chunk = enumerator.GetTextElement();
                var character = chunk[0];

                if (char.IsSurrogatePair(chunk, 0))
                {
                    var code = char.ConvertToUtf32(chunk, 0).ToString("x");
                    builder.Append(string.Format(format, code));
                }
                else if (character > 0x2600 && character < 0x27BF)
                {
                    var code = Convert.ToInt16(character).ToString("x");
                    builder.Append(string.Format(format, code));
                }
                else
                {
                    builder.Append(chunk);
                }

            }

            return builder.ToString();
        }

        public static string ParseTwitterUsername(this string s)
        {
            return Regex.Replace(s, "(@)((?:[A-Za-z0-9-_]*))", Username);
        }

        public static string ParseTwitterHashtag(this string s)
        {
            return Regex.Replace(s, @"(#)((?:[\w-æøåäö]*))", Hashtag, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }

        public static string ParseTwitterMessage(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return string.Empty;
            }

            return s.ParseURL().ParseTwitterHashtag().ParseTwitterUsername().ParseTwitterEmoji();
        }

        private static string Hashtag(Match m)
        {
            string x = m.ToString();
            return x.Link("https://twitter.com/search?q=" + Uri.EscapeDataString(x));
        }

        private static string Username(Match m)
        {
            string x = m.ToString();
            string username = x.Replace("@", "");
            return x.Link("https://twitter.com/" + username);
        }

        private static string URL(Match m)
        {
            string x = m.ToString();
            return x.Link(x);
        }
    }
}
