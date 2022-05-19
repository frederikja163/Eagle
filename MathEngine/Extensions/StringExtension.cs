using System.Collections.Generic;
using System.Linq;

namespace MathEngine.Extensions
{
    public static class StringExtension
    {
        public static string CleanExpressionString(this string str)
        {
            return str.Replace("  ", " ").Trim();
        }

        public static string[] SplitOuterScope(this string str, params char[] c)
        {
            return str.Split(c);
        }
    }
}