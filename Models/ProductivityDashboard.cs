using System;
using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class GetBillableGroupResults
    {
        [Key(0)]
        public int BillableGroupId { get; set; }
        [Key(1)]
        public string BillableGroup { get; set; }
    }

    [MessagePackObject]
    public class GetReporteeLocationMappingResults
    {
        [Key(0)]
        public int LocationID { get; set; }
        [Key(1)]
        public string Location { get; set; }
    }

    [MessagePackObject]
    public class GetReportingAuthorityResults
    {
        [Key(0)]
        public int ReportingAuthorityID { get; set; }
        [Key(1)]
        public string ReportingAuthorityName { get; set; }
    }


    [MessagePackObject]
    public class GetHourlyProductivityDashboardResult
    {
        [Key(0)]
        public int EmployeeID { get; set; }
        [Key(1)]
        public string WorkedBy { get; set; }
        [Key(2)]
        public int WorkedHour { get; set; }
        [Key(3)]
        public int st0 { get; set; }
        [Key(4)]
        public int st1 { get; set; }
        [Key(5)]
        public int st2 { get; set; }
        [Key(6)]
        public int st3 { get; set; }
        [Key(7)]
        public int st4 { get; set; }
        [Key(8)]
        public int st5 { get; set; }
        [Key(9)]
        public int st6 { get; set; }
        [Key(10)]
        public int st7 { get; set; }
        [Key(11)]
        public int st8 { get; set; }
        [Key(12)]
        public int st9 { get; set; }
        [Key(13)]
        public int st10 { get; set; }
        [Key(14)]
        public int st11 { get; set; }
        [Key(15)]
        public int st12 { get; set; }
        [Key(16)]
        public int st13 { get; set; }
        [Key(17)]
        public int st14 { get; set; }
        [Key(18)]
        public int st15 { get; set; }
        [Key(19)]
        public int st16 { get; set; }
        [Key(20)]
        public int st17 { get; set; }
        [Key(21)]
        public int st18 { get; set; }
        [Key(22)]
        public int st19 { get; set; }
        [Key(23)]
        public int st20 { get; set; }
        [Key(24)]
        public int st21 { get; set; }
        [Key(25)]
        public int st22 { get; set; }
        [Key(26)]
        public int st23 { get; set; }
        [Key(27)]
        public int TotalCount { get; set; }
    }

    [MessagePackObject]
    public class GetHourlyProductivityDashboardWithSubTaskResult
    {
        [Key(0)]
        public int EmployeeID { get; set; }
        [Key(1)]
        public string WorkedBy { get; set; }
        [Key(2)]
        public int WorkedHour { get; set; }
        [Key(3)]
        public int st0 { get; set; }
        [Key(4)]
        public int st1 { get; set; }
        [Key(5)]

        public int st2 { get; set; }
        [Key(6)]

        public int st3 { get; set; }
        [Key(7)]
        public int st4 { get; set; }
        [Key(8)]
        public int st5 { get; set; }
        [Key(9)]
        public int st6 { get; set; }
        [Key(10)]
        public int st7 { get; set; }
        [Key(11)]
        public int st8 { get; set; }
        [Key(12)]
        public int st9 { get; set; }
        [Key(13)]
        public int st10 { get; set; }
        [Key(14)]
        public int st11 { get; set; }
        [Key(15)]
        public int st12 { get; set; }
        [Key(16)]
        public int st13 { get; set; }
        [Key(17)]
        public int st14 { get; set; }
        [Key(18)]
        public int st15 { get; set; }
        [Key(19)]
        public int st16 { get; set; }
        [Key(20)]
        public int st17 { get; set; }
        [Key(21)]
        public int st18 { get; set; }
        [Key(22)]
        public int st19 { get; set; }
        [Key(23)]
        public int st20 { get; set; }
        [Key(24)]
        public int st21 { get; set; }
        [Key(25)]
        public int st22 { get; set; }
        [Key(26)]
        public int st23 { get; set; }
        [Key(27)]
        public int TotalCount { get; set; }
        [Key(28)]
        public string SubTask { get; set; }

        [Key(29)]
        public string Task { get; set; }
    }

    [MessagePackObject]
    public class GetHourlyEfficiencyDashboardResult
    {
        [Key(0)]
        public int EmployeeID { get; set; }
        [Key(1)]
        public string WorkedBy { get; set; }
        [Key(2)]
        public int WorkedHour { get; set; }
        [Key(3)]
        public decimal st0 { get; set; }
        [Key(4)]
        public decimal st1 { get; set; }
        [Key(5)]

        public decimal st2 { get; set; }
        [Key(6)]

        public decimal st3 { get; set; }
        [Key(7)]
        public decimal st4 { get; set; }
        [Key(8)]
        public decimal st5 { get; set; }
        [Key(9)]
        public decimal st6 { get; set; }
        [Key(10)]
        public decimal st7 { get; set; }
        [Key(11)]
        public decimal st8 { get; set; }
        [Key(12)]
        public decimal st9 { get; set; }
        [Key(13)]
        public decimal st10 { get; set; }
        [Key(14)]
        public decimal st11 { get; set; }
        [Key(15)]
        public decimal st12 { get; set; }
        [Key(16)]
        public decimal st13 { get; set; }
        [Key(17)]
        public decimal st14 { get; set; }
        [Key(18)]
        public decimal st15 { get; set; }
        [Key(19)]
        public decimal st16 { get; set; }
        [Key(20)]
        public decimal st17 { get; set; }
        [Key(21)]
        public decimal st18 { get; set; }
        [Key(22)]
        public decimal st19 { get; set; }
        [Key(23)]
        public decimal st20 { get; set; }
        [Key(24)]
        public decimal st21 { get; set; }
        [Key(25)]
        public decimal st22 { get; set; }
        [Key(26)]
        public decimal st23 { get; set; }
        [Key(27)]
        public int TotalCount { get; set; }

        [Key(28)]
        public decimal InternalTarget { get; set; }

        [Key(29)]
        public decimal AchievedPercentageWithFullTarget { get; set; }

        [Key(30)]
        public decimal AchievedPercentageWithCategoryTarget { get; set; }
    }
    [MessagePackObject]
    public class GetHourlyDashboardInput
    {
        [Key(0)]
        public string Schema { get; set; }
        [Key(1)]
        public int EmployeeID { get; set; }
        [Key(2)]
        public int ProjectgroupID { get; set; }
        [Key(3)]
        public string BillableGroup { get; set; }
        [Key(4)]
        public string Location { get; set; }
        [Key(5)]
        public string ReportingAuthority { get; set; }
        [Key(6)]
        public string ClientDisposition { get; set; }

    }
    [MessagePackObject]
    public class GetHourlyDashboardwithsubtaskInput
    {
        [Key(0)]
        public string Schema { get; set; }
        [Key(1)]
        public int EmployeeID { get; set; }
        [Key(2)]
        public int ProjectgroupID { get; set; }
        [Key(3)]
        public string BillableGroup { get; set; }
    }
    
    [MessagePackObject]
    public class GetHourlyAccountCountDashboardResult
    {
        [Key(0)]
        public int EmployeeID { get; set; }
        [Key(1)]
        public string WorkedBy { get; set; }
        [Key(2)]
        public int Total { get; set; }
        [Key(3)]
        public int stb0 { get; set; }
        [Key(4)]
        public int stb1 { get; set; }
        [Key(5)]
        public int stb2 { get; set; }
        [Key(6)]
        public int stb3 { get; set; }
        [Key(7)]
        public int stb4 { get; set; }
        [Key(8)]
        public int stb5 { get; set; }
        [Key(9)]
        public int stb6 { get; set; }
        [Key(10)]
        public int stb7 { get; set; }
        [Key(11)]
        public int stb8 { get; set; }
        [Key(12)]
        public int stb9 { get; set; }
        [Key(13)]
        public int stb10 { get; set; }
        [Key(14)]
        public int stb11 { get; set; }
        [Key(15)]
        public int stb12 { get; set; }
        [Key(16)]
        public int stb13 { get; set; }
        [Key(17)]
        public int stb14 { get; set; }
        [Key(18)]
        public int stb15 { get; set; }
        [Key(19)]
        public int stb16 { get; set; }
        [Key(20)]
        public int stb17 { get; set; }
        [Key(21)]
        public int stb18 { get; set; }
        [Key(22)]
        public int stb19 { get; set; }
        [Key(23)]
        public int stb20 { get; set; }
        [Key(24)]
        public int stb21 { get; set; }
        [Key(25)]
        public int stb22 { get; set; }
        [Key(26)]
        public int stb23 { get; set; }
        [Key(27)]
        public int WorkedHour { get; set; }
        [Key(28)]
        public int BufferColumn40 { get; set; }
        [Key(29)]
        public int PageNumber { get; set; }
    }

}
