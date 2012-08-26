using System;

namespace Interactors
{
    public static class DateTimeExtensions
    {
        public static DateTime CalculateNextSaturday(this DateTime now)
        {
            while (now.DayOfWeek != DayOfWeek.Saturday)
            {
                now = now.AddDays(1);
            }
            return now;
        }
    }
}