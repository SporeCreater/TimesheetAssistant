using System.Collections.Generic;

namespace Boundaries
{
    public class SubmitRequest
    {
        public List<string> DaysOfWeek { get; set; }
        public string Hours { get; set; }
        public string CurrentWeek { get; set; }
        public DayEntry DayEntry { get; set; }
    }
}