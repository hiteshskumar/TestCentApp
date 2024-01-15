using System;
using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class BatchAccounts
    {
        [Key(0)]
        public long UploadID { get; set; }
        [Key(1)]
        public long UploadTemplateID { get; set; }
        [Key(2)]
        public string? ChartIDOrFileID { get; set; }
        [Key(3)]
        public string? TotalCount { get; set; }
        [Key(4)]
        public string? PageNumber { get; set; }
        [Key(5)]
        public string? Comments { get; set; }
    }
    [MessagePackObject]
    public class SaveBatchAccounts
    {
        [Key(0)]
        public List<BatchAccounts>? Accounts { get; set; }
        [Key(1)]
        public string? SchemaName { get; set; }
        [Key(2)]
        public int ProjectGroupID { get; set; }
        [Key(3)]
        public int EmployeeID { get; set; }
        [Key(4)]
        public string? UniqueColumnValues { get; set; }
        [Key(5)]
        public string? SaveMode { get; set; }
        [Key(6)]
        public string? CommonComments { get; set; }
        
    }
    [MessagePackObject]
    public class GetSearchAndCodeResult
    {
       
        [Key(0)]
        public long AccountTransactionID { get; set; }
        [Key(1)]
        public long AccountDetailID { get; set; }
        [Key(2)]
        public int ProjectGroupID { get; set; }
        [Key(3)]
        public int TimesheetStructureID { get; set; }
        [Key(4)]
        public int UploadAccountID { get; set; }
        [Key(5)]
        public int UploadTemplateID { get; set; }
        [Key(6)]
        public string? PatientName { get; set; }
        [Key(7)]
        public DateTime? DOB { get; set; }
        [Key(8)]
        public int PatientAge { get; set; }
        [Key(9)]
        public string? PatientGender { get; set; }
        [Key(10)]
        public string? AccountNumber { get; set; }
        [Key(11)]
        public string? MRNNumber { get; set; }
        [Key(12)]
        public string? ChartIDOrFileID { get; set; }
        [Key(13)]
        public string? Insurance { get; set; }
        [Key(14)]
        public string? SSN { get; set; }
        [Key(15)]
        public string? FacilityOrHospital { get; set; }
        [Key(16)]
        public string? Location { get; set; }
        [Key(17)]
        public string? ProviderName { get; set; }
        [Key(18)]
        public string? ReferringProvider { get; set; }
        [Key(19)]
        public string? SupervisingProvider { get; set; }
        [Key(20)]
        public string? AttendingProvider { get; set; }
        [Key(21)]
        public string? BillingProvider { get; set; }
        [Key(22)]
        public string? NPI { get; set; }
        [Key(23)]
        public DateTime? AdmitDate { get; set; }
        [Key(24)]
        public DateTime? DischargeDate { get; set; }
        [Key(25)]
        public DateTime? DOS { get; set; }
        [Key(26)]
        public string? Comments { get; set; }
        [Key(27)]
        public string? ReasonOrRemarkCode { get; set; }
        [Key(28)]
        public string? StartTime { get; set; }
        [Key(29)]
        public string? StopTime { get; set; }
        [Key(30)]
        public string? TimeDifference { get; set; }
        [Key(31)]
        public string? TimeUnits { get; set; }
        [Key(32)]
        public string? PhysicalStatus { get; set; }
        [Key(33)]
        public string? IsEmergency { get; set; }
        [Key(34)]
        public string? ASAModifier { get; set; }
        [Key(35)]
        public string? BatchID { get; set; }
        [Key(36)]
        public string? TotalCount { get; set; }
        [Key(37)]
        public string? PageNumber { get; set; }
        [Key(38)]
        public string? POS { get; set; }
        [Key(39)]
        public string? BufferColumn1 { get; set; }
        [Key(40)]
        public string? BufferColumn2 { get; set; }
        [Key(41)]
        public string? BufferColumn3 { get; set; }
        [Key(42)]
        public string? BufferColumn4 { get; set; }
        [Key(43)]
        public string? BufferColumn5 { get; set; }
        [Key(44)]
        public string? BufferColumn6 { get; set; }
        [Key(45)]
        public string? BufferColumn7 { get; set; }
        [Key(46)]
        public string? BufferColumn8 { get; set; }
        [Key(47)]
        public string? BufferColumn9 { get; set; }
        [Key(48)]
        public string? BufferColumn10 { get; set; }
        [Key(49)]
        public string? BufferColumn11 { get; set; }
        [Key(50)]
        public string? BufferColumn12 { get; set; }
        [Key(51)]
        public string? BufferColumn13 { get; set; }
        [Key(52)]
        public string? BufferColumn14 { get; set; }
        [Key(53)]
        public string? BufferColumn15 { get; set; }
        [Key(54)]
        public string? BufferColumn16 { get; set; }
        [Key(55)]
        public string? BufferColumn17 { get; set; }
        [Key(56)]
        public string? BufferColumn18 { get; set; }
        [Key(57)]
        public string? BufferColumn19 { get; set; }
        [Key(58)]
        public string? BufferColumn20 { get; set; }
        [Key(59)]
        public string? BufferColumn39 { get; set; }
        [Key(60)]
        public string? BufferColumn40 { get; set; }
        [Key(61)]
        public string? BufferColumn41 { get; set; }
        [Key(62)]
        public string? BufferColumn42 { get; set; }
        [Key(63)]
        public string? BufferColumn43 { get; set; }
        [Key(64)]
        public string? BufferColumn44 { get; set; }
        [Key(65)]
        public string? BufferColumn45 { get; set; }
        [Key(66)]
        public string? BufferColumn46 { get; set; }
        [Key(67)]
        public string? BufferColumn47 { get; set; }
        [Key(68)]
        public string? BufferColumn48 { get; set; }
        [Key(69)]
        public string? BufferColumn49 { get; set; }
        [Key(70)]
        public string? BufferColumn50 { get; set; }
        [Key(71)]
        public string? BufferColumn51 { get; set; }
        [Key(72)]
        public string? BufferColumn52 { get; set; }
        [Key(73)]
        public string? BufferColumn53 { get; set; }
        [Key(74)]
        public string? BufferColumn54 { get; set; }
        [Key(75)]
        public string? BufferColumn55 { get; set; }
        [Key(76)]
        public string? BufferColumn56 { get; set; }
        [Key(77)]
        public string? BufferColumn57 { get; set; }
        [Key(78)]
        public string? BufferColumn58 { get; set; }
        [Key(79)]
        public int EMCode { get; set; }
        [Key(80)]
        public int EMModifier { get; set; }
        [Key(81)]
        public int ProcedureCode { get; set; }
        [Key(82)]
        public int ProcedureModifier { get; set; }
        [Key(83)]
        public int ICD10CMCodes { get; set; }
        [Key(84)]
        public int Units { get; set; }
        [Key(85)]
        public int ERCode { get; set; }
        [Key(86)]
        public int ERModifier { get; set; }
        [Key(87)]
        public int CPTCode { get; set; }
        [Key(88)]
        public string? Modifier { get; set; }
        [Key(89)]
        public int ASACode { get; set; }
        [Key(90)]
        public int ASAUnit { get; set; }
        [Key(91)]
        public int PhysicalStatusUnits { get; set; }
        [Key(92)]
        public string? Emergency { get; set; }
        [Key(93)]
        public int EmergencyUnit { get; set; }
        [Key(94)]
        public int VentCPT { get; set; }
        [Key(95)]
        public int VentCPTUnits { get; set; }
        [Key(96)]
        public int PQRS { get; set; }
        [Key(97)]
        public int Block { get; set; }
        [Key(98)]
        public int Ultrasound { get; set; }
        [Key(99)]
        public string? TEE { get; set; }
        [Key(100)]
        public int Lines { get; set; }
        [Key(101)]
        public string? AccountType { get; set; }
        [Key(102)]
        public int AccountStatusID { get; set; }
        [Key(103)]
        public string? ClientLogin { get; set; }
        [Key(104)]
        public Decimal InternalTarget { get; set; }
        [Key(105)]
        public Decimal RampUpTarget { get; set; }
        [Key(106)]
        public string? SubmittedFrom { get; set; }
        [Key(107)]
        public Boolean IsIgnoreAndSave { get; set; }
        [Key(108)]
        public int UpdatedBy { get; set; }
        [Key(109)]
        public DateTime UpdatedDateTimeEST { get; set; }
        [Key(110)]
        public DateTime UpdatedDateTimeGMT { get; set; }
        [Key(111)]
        public int TimeSpent { get; set; }
        [Key(112)]
        public Byte FirstLevelAuditStatusID { get; set; }

        [Key(113)]
        public string? Department { get; set; }
        [Key(114)]
        public string? Process { get; set; }
        [Key(115)]
        public string? Client { get; set; }
        [Key(116)]
        public string? SubClient { get; set; }
        [Key(117)]
        public string? Software { get; set; }
        [Key(118)]
        public string? BillableGroup { get; set; }
        [Key(119)]
        public string? task { get; set; }
        [Key(120)]
        public string? subtask { get; set; }
        [Key(121)]
        public int taskid { get; set; }
        [Key(122)]
        public int subtaskid { get; set; }
        
        [Key(123)]
        public string? Workedby { get; set; }
        
        [Key(124)]
        public DateTime UpdatedDateTimeIST { get; set; }
        
        [Key(125)]
        public string? AccountStatus { get; set; }
        
        [Key(126)]
        public DateTime WorkedDate { get; set; }
       
        [Key(127)]
        public string? SchemaName { get; set; }
        
        [Key(128)]
        public int ClientID { get; set; }

        [Key(129)]
        public int SubClientID { get; set; }

        [Key(130)]
        public int SoftwareID { get; set; }

        [Key(131)]
        public int BillableGroupID { get; set; }

        [Key(132)]
        public long? AccountHistoryID { get; set; }

        [Key(134)]
        public int UploadID {get; set;}

        [Key(135)]
        public bool IsChecked { get; set; }

        [Key(136)]
        public string? AssignedDate { get; set; }

        [Key(137)]
        public string? Employee { get; set; }
        
        [Key(138)]
        public string? Assigned {get; set;}
    }
    [MessagePackObject]
    public class SearchAndCodeAccountDetails
    {
        [Key(0)]
        public IEnumerable<GetFieldResult>? FieldResult { get; set; }

        [Key(1)]
        public IEnumerable<GetSearchAndCodeResult>? SearchResult { get; set; }
        
        [Key(2)]
        public string? Message { get; set; }
         
    }
    [MessagePackObject]
    public class EditableFields
    {
        [Key(0)]
        public string? CodingField { get; set; }

        [Key(1)]
        public bool IsEditable { get; set; }
    }
    [MessagePackObject]
    public class SearchAndCodeAllocatedAccounts
    {
        [Key(0)]
        public IEnumerable<EmployeeAccountDetails> allocatedAccounts { get; set; }

        [Key(1)]
        public IEnumerable<EditableFields> editableFields { get; set; }
        
        [Key(3)]
        public bool HasSkip { get; set; }
    }
    [MessagePackObject]
    public class SoftwareUserLoginAccess
    {
        [Key(0)]
        public string? ClientLogin { get; set; }

        [Key(1)]
        public int UserID { get; set; }
       
        [Key(2)]
        public int SoftwareUserLoginID { get; set; }

        [Key(3)]
        public int ClientSubClientID { get; set; }

        [Key(4)]
        public int  LCMProcessID { get; set; }

        [Key(5)]
        public int  ProcessID { get; set; }

        [Key(6)]
        public int  PGMClientID { get; set; }

        [Key(7)]
        public int  CSCClientID { get; set; }
        
        [Key(8)]
        public int Status { get; set; }
        
        [Key(9)]
        public string? StatusMessage { get; set; }

        [Key(10)]
        public int ProjectGroupID { get; set; }
    }

     [MessagePackObject]
     public class GridSearchParameters
     {
      
      [Key(0)]
      public int ProjectGroupID { get; set; }

      [Key(1)]
      public string? SchemaName { get; set; }

      [Key(2)]
      public int EmployeeID { get; set; }
      
      [Key(3)]
      public IEnumerable<SearchCriteria> SearchCriterias { get; set; } = null!;

      [Key(4)]
      public string? SearchValue { get; set; } //a.MRN_Number like '%2022%'
      
     }
}