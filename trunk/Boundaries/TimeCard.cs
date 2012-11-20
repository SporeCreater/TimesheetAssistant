using System.Collections.Generic;

namespace Boundaries
{
    public class TimeCard
    {
        private readonly bool _isEmpty;

        public TimeCard(bool isEmpty = false)
        {
            _isEmpty = isEmpty;
        }

        public string CurrentWeek { get; set; }
        public List<string> WeekDays { get; set; }
        public List<string> EarningCodes { get; set; }
        public List<string> ContractLines { get; set; }
        public List<string> ContractNumbers { get; set; }
        public List<string> ActivityIDs { get; set; }
        public List<string> ProjectIDs { get; set; }

        public static TimeCard Empty
        {
            get { return new TimeCard(true); }         
        }

        public bool IsEmpty()
        {
            return _isEmpty;
        }
    }
}