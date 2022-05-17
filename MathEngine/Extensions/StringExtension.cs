namespace MathEngine.Extensions
{
    public static class StringExtension
    {
        public static string CleanExpressionString(this string txt)
        {
            return txt.Replace("  ", " ").Trim();
        }
    }
}