using System;

namespace MealOrdering.Shared.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNull(this Guid Value)
        {
            return Value == null || Value == Guid.Empty;
        }

        public static bool IsNull(this Guid? Value)
        {
            return Value == null || !Value.HasValue || Value == Guid.Empty;
        }





        public static string SafeSubstring(this string Text, int Start, int Length)
        {
            return Text.Length <= Start ? ""
                : Text.Length - Start <= Length ? Text.Substring(Start)
                : Text.Substring(Start, Length);
        }

        public static string SafeSubstring(this string Text, int Length)
        {
            if (string.IsNullOrEmpty(Text)) return Text;
            return Text.Length <= Length ? Text : Text.Substring(0, Length);
        }
    }
}
