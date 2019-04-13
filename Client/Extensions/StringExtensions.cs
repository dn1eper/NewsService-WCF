namespace Client.Extensions
{
    static class StringExtensions
    {
        public static bool IsEmpty(this string text)
        {
            return text.Trim().Length == 0;
        }
    }
}
