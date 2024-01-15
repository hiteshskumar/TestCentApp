using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class BusinessHoursReportInput
    {
        [Key(0)]
        public int projectgroupID { get; set; }
        [Key(1)]
        public DateTime FromDate { get; set; }
        [Key(2)]
        public DateTime ToDate { get; set; }

    }

    [MessagePackObject]
    public class ProductivityBusinessHoursReport
    {
        [Key(0)]
        public int EmployeeID { get; set; }
        [Key(1)]
        public string Employee { get; set; } = string.Empty;
        [Key(2)]
        public string ShiftTime { get; set; } = string.Empty;
        [Key(3)]
        public int? Total { get; set; }
        [Key(4)]
        public int? st1 { get; set; } = 0;
        [Key(5)]
        public int? st2 { get; set; } = 0;
        [Key(6)]
        public int? st3 { get; set; } = 0;
        [Key(7)]
        public int? st4 { get; set; } = 0;
        [Key(8)]
        public int? st5 { get; set; } = 0;
        [Key(9)]
        public int? st6 { get; set; } = 0;
        [Key(10)]
        public int? st7 { get; set; } = 0;
        [Key(11)]
        public int? st8 { get; set; } = 0;
        [Key(12)]
        public int? st9 { get; set; } = 0;
        [Key(13)]
        public int? st10 { get; set; } = 0;
        [Key(14)]
        public int? st11 { get; set; } = 0;
        [Key(15)]
        public int? st12 { get; set; } = 0;
        [Key(16)]
        public int? st13 { get; set; } = 0;
        [Key(17)]
        public int? st14 { get; set; } = 0;
        [Key(18)]
        public int? st15 { get; set; } = 0;
        [Key(19)]
        public int? st16 { get; set; } = 0;
        [Key(20)]
        public int? st17 { get; set; } = 0;
        [Key(21)]
        public int? st18 { get; set; } = 0;
        [Key(22)]
        public int? st19 { get; set; } = 0;
        [Key(23)]
        public int? st20 { get; set; } = 0;
        [Key(24)]
        public int? st21 { get; set; } = 0;
        [Key(25)]
        public int? st22 { get; set; } = 0;
        [Key(26)]
        public int? st23 { get; set; } = 0;
        [Key(27)]
        public int? st24 { get; set; } = 0;

    }
    public class EmployeeMTDDataInput
    {
        public EmployeeMTDDataInput()
        {
            AuditLevel = 1;
            Mode = 1;
            ScoringColumnName = "UIScoringMethodID";
            FromDate = DateTime.Now;
            ToDate = DateTime.Now;
        }
        [Key(0)]
        public int AuditLevel { get; set; }
        [Key(1)]
        public int Mode { get; set; }
        [Key(2)] public string ScoringColumnName { get; set; }
        [Key(3)] public int EmployeeID { get; set; }
        [Key(4)] public DateTime FromDate { get; set; }
        [Key(5)] public DateTime ToDate { get; set; }
        [Key(6)] public string FilterCondition { get; set; }
    }
    [MessagePackObject]
    public class EmployeeMTDData
    {
        [Key(0)]
        public string ProductionAgent { get; set; }
        [Key(1)]
        public string Reviewer { get; set; }
        [Key(2)]
        public string ProductionDate { get; set; }
        [Key(3)]
        public int? TotalWorked { get; set; }
        [Key(4)]
        public int? TotalQCd { get; set; }
        [Key(5)]
        public int? ErrorCount { get; set; }
        [Key(6)]

        public decimal SamplePer { get; set; }
        [Key(7)]

        public decimal TotalErrorPoints { get; set; }
        [Key(8)]

        public decimal TotalPossiblePoints { get; set; }
        [Key(9)]


        public decimal QualityScore { get; set; }
        [Key(10)]
        public int? SumTotalWorked { get; set; }
        [Key(11)]
        public int SumTotalWorkedCount { get; set; }

    }

}