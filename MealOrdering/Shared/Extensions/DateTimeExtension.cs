using System;

namespace MealOrdering.Shared.Extensions
{
    public static class DateTimeExtension
    {
        private static string emptyDateTimeStr = "00:00:00";

        public static string GetRemaningDateStr(this DateTime ExpireDate)
        {
            if (ExpireDate == null || ExpireDate == DateTime.MinValue)
                return emptyDateTimeStr;

            TimeSpan ts = ExpireDate.Subtract(DateTime.Now);

            return ts.TotalSeconds >= 0 ? ts.ToString(@"hh\:mm\:ss") : emptyDateTimeStr;
        }

        public static string ToCustomDateString(this DateTime DateTime)
        {
            return DateTime == null ? "01.01.2000" : DateTime.ToString("dd.MM.yyyy");
        }

        public static string ToCustomTimeString(this DateTime DateTime)
        {
            return DateTime == null ? "00:00" : DateTime.ToString("HH:mm");
        }

        public static string ToCustomDateTimeString(this DateTime DateTime)
        {
            return $"{DateTime.ToCustomDateString()} {DateTime.ToCustomTimeString()}";
        }

        public static bool IsNull(this DateTime DateTime)
        {
            return DateTime == null || DateTime == DateTime.MinValue;
        }

        public static bool IsNull(this DateTime? DateTime)
        {
            return !DateTime.HasValue || IsNull(DateTime);
        }
    }
}
