using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleEndpoints.Extensions
{
    internal static class StringExtensions
    {
        public static string ReplaceCaseInsensitive(this string input, string search, string replacement)
        {
            return Regex.Replace(
                input,
                Regex.Escape(search),
                replacement.Replace("$", "$$"),
                RegexOptions.IgnoreCase
            );
        }
    }
}
