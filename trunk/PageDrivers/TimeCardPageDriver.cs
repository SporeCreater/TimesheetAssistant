using WatiN.Core;

namespace PageDrivers
{
    public class TimeCardPageDriver: PageDriver
    {
        public WatinSelectList WeekEndingList { get; private set; }

        public TimeCardPageDriver(IE ie)
        {
            WeekEndingList = new WatinSelectList(ie, this, "WeekEnding1");
        }

    }
}