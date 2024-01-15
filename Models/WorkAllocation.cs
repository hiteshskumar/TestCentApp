using System;
using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class WorkallocationAccountSummary
    {
        [Key(0)]
        public int Assignedaccounts { get; set; }
        [Key(1)]
        public int Completed { get; set; }
    }
    [MessagePackObject]
    public class WorkallocationRuleMaster
    {
        [Key(0)]
        public int ProjectGroupID { get; set; }
        [Key(1)]
        public int EmployeeID { get; set; }
        [Key(2)]
        public int UploadTemplateID { get; set; }
        [Key(3)]
        public bool AllocateToPreviouslyWorkedAgents { get; set; }
        [Key(4)]
        public bool AllocateToIdleAgents { get; set; }
        [Key(5)]
        public int WorkAllocationRuleID { get; set; }
        [Key(6)]
        public string? AllocationMethod { get; set; }
        [Key(7)]
        public int RulePriority { get; set; }
        [Key(8)]
        public string? AllocationType { get; set; } 
        [Key(9)]
        public string? WorkAllocationRule { get; set; }
        [Key(10)]
        public string? RuleCondition { get; set; }
        [Key(11)]
        public string? AllocationOrder { get; set; }
        [Key(12)]
        public bool IsFetched { get; set; }
        [Key(13)]
        public short? SkipReasonID { get; set; }
        [Key(14)]
        public bool IsCompleted { get; set; }
        [Key(15)]
        public string? AccountNumber { get; set; }
        [Key(16)]
        public int UploadID { get; set; }
        [Key(17)]
        public long AccountDetailID { get; set; }
        [Key(18)]
        public int AssignedTo { get; set; }
        [Key(19)]
        public long RowNum { get; set; }
        [Key(20)]
        public int LastWorkedBy { get; set; }
        [Key(21)]
        public string? Rulequery{ get; set; }        
        [Key(22)]
        public bool AllocateToPreviouslyWorkedAents { get; set; }
    }
    [MessagePackObject]
    public class WorkAllocationExcelTemplate
    {
        [Key(0)]
        public IEnumerable<UploadExcelColumns>? ColumnNames { get; set; }
        [Key(1)]
        public IEnumerable<UploadAccountDetails>? UploadAccountDetails { get; set; }
    }
    [MessagePackObject]
    public class UploadExcelColumns
    {
        [Key(0)]
        public string? DisplayName { get; set; }
        [Key(1)]
        public string? FieldName { get; set; }
    }
    [MessagePackObject]    
    public class UploadAccountDetails
    {        
        [Key(0)]
        public long UploadID { get; set; }

        [Key(1)]
        public long AccountDetailID { get; set; }

        [Key(2)]
        public int UploadTemplateID { get; set; }

        [Key(3)]
        public string? PatientName { get; set; }

        [Key(4)]
        public DateTime DOB { get; set; }

        [Key(5)]
        public int PatientAge { get; set; }

        [Key(6)]
        public string? PatientGender { get; set; }

        [Key(7)]
        public string? AccountNumber { get; set; }

        [Key(8)]
        public string? MRNNumber { get; set; }

        [Key(9)]
        public string? ChartIDOrFileID { get; set; }

        [Key(10)]
        public string? Insurance { get; set; }

        [Key(11)]
        public string? SSN { get; set; }

        [Key(12)]
        public string? FacilityOrHospital { get; set; }

        [Key(13)]
        public string? Location { get; set; }

        [Key(14)]
        public string? ProviderName { get; set; }

        [Key(15)]
        public string? ReferringProvider { get; set; }

        [Key(16)]
        public string? SupervisingProvider { get; set; }

        [Key(17)]
        public string? AttendingProvider { get; set; }

        [Key(18)]
        public string? BillingProvider { get; set; }

        [Key(19)]
        public string? NPI { get; set; }

        [Key(20)]
        public DateTime AdmitDate { get; set; }

        [Key(21)]
        public DateTime DischargeDate { get; set; }

        [Key(22)]
        public DateTime DOS { get; set; }

        [Key(23)]
        public string? Comments { get; set; }

        [Key(24)]
        public string? ReasonOrRemarkCode { get; set; }

        [Key(25)]
        public string? StartTime { get; set; }

        [Key(26)]
        public string? StopTime { get; set; }

        [Key(27)]
        public string? TimeDifference { get; set; }
        
        [Key(28)]
        public string? TimeUnits { get; set; }

        [Key(29)]
        public string? PhysicalStatus { get; set; }
        
        [Key(30)]
        public string? IsEmergency { get; set; }

        [Key(31)]
        public string? ASAModifier { get; set; }

        [Key(32)]
        public string? BatchID { get; set; }

        [Key(33)]
        public string? TotalCount { get; set; }
        
        [Key(34)]
        public string? PageNumber { get; set; }

        [Key(35)]
        public string? POS { get; set; }

        [Key(36)]
        public string? BufferColumn1 { get; set; }

        [Key(37)]
        public string? BufferColumn2 { get; set; }

        [Key(38)]
        public string? BufferColumn3 { get; set; }
        
        [Key(39)]
        public string? BufferColumn4 { get; set; }

        [Key(40)]
        public string? BufferColumn5 { get; set; }

        [Key(41)]
        public string? BufferColumn6 { get; set; }

        [Key(42)]
        public string? BufferColumn7 { get; set; }

        [Key(43)]
        public string? BufferColumn8 { get; set; }

        [Key(44)]
        public string? BufferColumn9 { get; set; }

        [Key(45)]
        public string? BufferColumn10 { get; set; }

        [Key(46)]
        public string? BufferColumn11 { get; set; }

        [Key(47)]
        public string? BufferColumn12 { get; set; }

        [Key(48)]
        public string? BufferColumn13 { get; set; }
        
        [Key(49)]
        public string? BufferColumn14 { get; set; }

        [Key(50)]
        public string? BufferColumn15 { get; set; }

        [Key(51)]
        public string? BufferColumn16 { get; set; }

        [Key(52)]
        public string? BufferColumn17 { get; set; }
        
        [Key(53)]
        public string? BufferColumn18 { get; set; }

        [Key(54)]
        public string? BufferColumn19 { get; set; }

        [Key(55)]
        public string? BufferColumn20 { get; set; }

        [Key(56)]
        public string? BufferColumn39 { get; set; }

        [Key(57)]
        public string? BufferColumn40 { get; set; }

        [Key(58)]
        public string? BufferColumn41 { get; set; }

        [Key(59)]
        public string? BufferColumn42 { get; set; }

        [Key(60)]
        public string? BufferColumn43 { get; set; }

        [Key(61)]
        public string? BufferColumn44 { get; set; }

        [Key(62)]
        public string? BufferColumn45 { get; set; }

        [Key(63)]
        public string? BufferColumn46 { get; set; }
        
        [Key(64)]
        public string? BufferColumn47 { get; set; }

        [Key(65)]
        public string? BufferColumn48 { get; set; }

        [Key(66)]
        public string? BufferColumn49 { get; set; }

        [Key(67)]
        public string? BufferColumn50 { get; set; }

        [Key(68)]
        public string? BufferColumn51 { get; set; }

        [Key(69)]
        public string? BufferColumn52 { get; set; }

        [Key(70)]
        public string? BufferColumn53 { get; set; }

        [Key(71)]
        public string? BufferColumn54 { get; set; }
        
        [Key(72)]
        public string? BufferColumn55 { get; set; }

        [Key(73)]
        public string? BufferColumn56 { get; set; }

        [Key(74)]
        public string? BufferColumn57 { get; set; }

        [Key(75)]
        public string? BufferColumn58 { get; set; }

        [Key(76)]
        public int EMCode { get; set; }
        
        [Key(77)]
        public int EMModifier { get; set; }
        
        [Key(78)]
        public int ProcedureCode { get; set; }

        [Key(79)]
        public int ProcedureModifier { get; set; }

        [Key(80)]
        public int ICD10CMCodes { get; set; }

        [Key(81)]
        public int Units { get; set; }

        [Key(82)]
        public int ERCode { get; set; }

        [Key(83)]
        public int ERModifier { get; set; }

        [Key(84)]
        public int CPTCode { get; set; }

        [Key(85)]
        public string? Modifier { get; set; }

        [Key(86)]
        public int ASACode { get; set; }

        [Key(87)]
        public int ASAUnit { get; set; }

        [Key(88)]
        public int PhysicalStatusUnits { get; set; }

        [Key(89)]
        public string? Emergency { get; set; }

        [Key(90)]
        public int EmergencyUnit { get; set; }

        [Key(91)]
        public int VentCPT { get; set; }

        [Key(92)]
        public int VentCPTUnits { get; set; }

        [Key(93)]
        public int PQRS { get; set; }

        [Key(94)]
        public int Block { get; set; }
        
        [Key(95)]
        public int Ultrasound { get; set; }

        [Key(96)]
        public string? TEE { get; set; }

        [Key(97)]
        public int Lines { get; set; }

        [Key(98)]
        public int SubClientID { get; set; }

        [Key(99)]
        public int BillableGroupID { get; set; }

        [Key(100)]
        public int AssignedTo { get; set; }

        [Key(101)]
        public int LastWorkedBy { get; set; }

        [Key(102)]
        public int UpdatedBy { get; set; }

        [Key(103)]
        public DateTime UpdatedDateTimeEST { get; set; }

        [Key(104)]
        public DateTime UpdatedDateTimeIST { get; set; }

        [Key(105)]
        public DateTime UpdatedDateTimeGMT { get; set; }

        [Key(106)]
        public string? SubClient { get; set; }

        [Key(107)]
        public string? BillableGroup { get; set; }

        [Key(108)]
        public string? task { get; set; }

        [Key(109)]
        public string? subtask { get; set; }

        [Key(110)]
        public string? Software { get; set; }
        
        [Key(111)]
        public int SoftwareID { get; set; }

        [Key(112)]
        public bool SkipReasonFlag { get; set; }

        [Key(113)]
        public bool CompleteBatchFlag { get; set; }

        [Key(114)]
        public string? AccountType { get; set; }
        [Key(115)]
        public string? Employee { get; set; }
        // [Key(116)]
        // public string? InsertedFrom { get; set; }
    }
    [MessagePackObject]
    public class AccountSummary
    {
        [Key(0)]
        public int  Accountcounts { get; set; }
        [Key(1)]
        public string ? Username { get; set; }
         [Key(2)]
        public int EmployeeID { get; set; }
    }
    [MessagePackObject]
    public class WorkAllocationAccounts
    {
        [Key(0)]
        public IEnumerable<EmployeeAccountDetails>? AllocatedAccounts { get; set; }

        [Key(1)]
        public IEnumerable<EditableFields>? EditableFields { get; set; }
        
        [Key(2)]
        public bool HasSkip { get; set; } 
    }
}