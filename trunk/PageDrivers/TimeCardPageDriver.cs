using WatiN.Core;

namespace PageDrivers
{
    public class TimeCardPageDriver: PageDriver
    {
        public WatinSelectList WeekEndingList { get; private set; }
        public WatinSelectList WeekDateList { get; private set; }
        public WatinSelectList EarningCodes { get; private set; }
        public WatinSelectList ContractLines { get; private set; }
        public WatinSelectList ContractNumbers { get; private set; }
        public WatinSelectList ActivityIDs { get; private set; }
        public WatinSelectList ProjectIDs { get; private set; }

        public TimeCardPageDriver(IE ie)
        {
            WeekEndingList = new WatinSelectList(ie, this, "WeekEnding1");
            WeekDateList = new WatinSelectList(ie, this, "repHourlyTimeCards_ctl00_uclHourlyTime_ddlDate");
            EarningCodes = new WatinSelectList(ie, this, "repHourlyTimeCards_ctl00_uclHourlyTime_ddlEarnCode");
            ContractLines = new WatinSelectList(ie, this, "repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf1_ddlValue");
            ContractNumbers = new WatinSelectList(ie, this, "repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf2_ddlValue");
            ActivityIDs = new WatinSelectList(ie, this, "repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf3_ddlValue");
            ProjectIDs = new WatinSelectList(ie, this, "repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf4_ddlValue");
        }

    }
}