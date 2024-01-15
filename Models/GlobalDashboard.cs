using System;
using MessagePack;

namespace ChargesWFM.UI.Models
{

    [MessagePackObject]
    public class GetProcessclientResults
    {
        [Key(0)]
        public string? Process { get; set; }
        [Key(1)]
        public string? Client { get; set; }
        [Key(2)]
        public int DepartmentID { get; set; }
        [Key(3)]
        public int ProcessID { get; set; }
        [Key(4)]
        public int ClientID { get; set; }
    }

    [MessagePackObject]
    public class GetGlobalDashboardResult
    {
        [Key(0)]
        public bool DisplaySubGrid { get; set; }

        [Key(1)]
        public IEnumerable<ParentDashboardResult> ProductionDataCountResult { get; set; }

        [Key(2)]
        public string? projectgroup { get; set; }
 
    }

    [MessagePackObject]
    public class GlobalProjectGroupsSummary
    {
        [Key(0)]
        public IEnumerable<ParentDashboardResult> ProductionParentResult { get; set; }
    }
    [MessagePackObject]
    public class tblProjectGroups
    {
        [Key(0)]
        public int ProjectGroupID { get; set; }

        [Key(1)]
        public string ProjectGroup { get; set; }

        [Key(2)]
        public string ProjectGroupCode { get; set; }

        [Key(3)]
        public int DatabaseID { get; set; }

    }
    [MessagePackObject] 
    public class ParentDashboardResult
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
        public int Projectgroupid { get; set; }
        [Key(29)]
        public bool DisplayProductionSubGrid { get; set; }

    }
    
    [MessagePackObject]
    public class EmployeewiseProductionDashboardResult
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
        public int Projectgroupid { get; set; }
    }
    [MessagePackObject]
    public class EmployeewiseBacklogDashboardResult
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
        public int Projectgroupid { get; set; }
    }
    [MessagePackObject]
    public class EmployeeWiseEfficiencyDashboardResult
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
    public class Efficiency
    {
        [Key(0)]
        public string? ProjectGroup { get; set; }
        [Key(1)]
        public string? WorkedBy { get; set; }
        [Key(2)]
        public decimal AchievedPercentageWithFullTarget { get; set; }
        [Key(3)]
        public decimal AchievedPercentageWithCategoryTarget { get; set; }
        [Key(4)]
        public int Total { get; set; }
        [Key(5)]
        public decimal st0 { get; set; }
        [Key(6)]
        public decimal st1 { get; set; }
        [Key(7)]
        public decimal st2 { get; set; }
        [Key(8)]
        public decimal st3 { get; set; }
        [Key(9)]
        public decimal st4 { get; set; }
        [Key(10)]
        public decimal st5 { get; set; }
        [Key(11)]
        public decimal st6 { get; set; }
        [Key(12)]
        public decimal st7 { get; set; }
        [Key(13)]
        public decimal st8 { get; set; }
        [Key(14)]
        public decimal st9 { get; set; }
        [Key(15)]
        public decimal st10 { get; set; }
        [Key(16)]
        public decimal st11 { get; set; }
        [Key(17)]
        public decimal st12 { get; set; }
        [Key(18)]
        public decimal st13 { get; set; }
        [Key(19)]
        public decimal st14 { get; set; }
        [Key(20)]
        public decimal st15 { get; set; }
        [Key(21)]
        public decimal st16 { get; set; }
        [Key(22)]
        public decimal st17 { get; set; }
        [Key(23)]
        public decimal st18 { get; set; }
        [Key(24)]
        public decimal st19 { get; set; }
        [Key(25)]
        public decimal st20 { get; set; }
        [Key(26)]
        public decimal st21 { get; set; }
        [Key(27)]
        public decimal st22 { get; set; }
        [Key(28)]
        public decimal st23 { get; set; }
    }

    [MessagePackObject]
    public class Data
    {
        [Key(0)]
        public string? ProjectGroup { get; set; }
        [Key(1)]
        public string? WorkedBy { get; set; }
        [Key(2)]
        public int Total { get; set; }
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
    }

    [MessagePackObject]
    public class CountSummary
    {
        [Key(0)]
        public string? ProjectGroup { get; set; }
        [Key(1)]
        public string? Type { get; set; }
        [Key(2)]
        public int Total { get; set; }
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
    }

    [MessagePackObject]
    public class GetGlobalDashboardDownloadResult
    {
        [Key(0)]
        public IEnumerable<CountSummary>? Summary { get; set; }

        [Key(1)]
        public IEnumerable<Data>? Backlog { get; set; }
        [Key(2)]
        public IEnumerable<Data>? CompletedAccounts { get; set; }
        [Key(3)]
        public IEnumerable<Efficiency>? Efficiency { get; set; }
    }
}
