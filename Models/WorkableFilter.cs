using System;
using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class UploadWorkableFilterdetails
    {
        [Key(0)]
        public IEnumerable<WorkableErrorDetails> UploadResult { get; set; }
        [Key(1)]
        public SuccessMessage Success { get; set; } 
    }
     public class SuccessMessage
    {
        public string Status { get; set; }        
    }
    [MessagePackObject]
    public class WorkableErrorDetails
    {
         [Key(0)]
        public string Errordetails { get; set; }
        [Key(1)]
        public string SubClient { get; set; }
        [Key(2)]
        public string BillableGroup { get; set; }

        [Key(3)]
        public string MeditechID { get; set; }
    }

    [MessagePackObject]
    public class DownloadWorkableFilterdetails
    {
        [Key(0)]
        public IEnumerable<GetFieldResult> UniqueColumns { get; set; }

        [Key(1)]
        public IEnumerable<WorkableFiltertbl> result { get; set; }
    }

    [MessagePackObject]
    public class WorkableFiltertbl
    {
        [Key(0)]
        public string ErrorMessage { get; set; }
        [Key(1)]
        public long UploadID { get; set; }
        [Key(2)]
        public int? ProjectGroupID { get; set; }
        [Key(3)]
        public string Location { get; set; }
        [Key(4)]
        public string PatientName { get; set; }
        [Key(5)]
        public string DOB { get; set; }
        [Key(6)]
        public string PatientAge { get; set; }
        [Key(7)]
        public string PatientGender { get; set; }
        [Key(8)]
        public string AccountNumber { get; set; }
        [Key(9)]
        public string MRNNumber { get; set; }
        [Key(10)]
        public string ChartIDOrFileID { get; set; }
        [Key(11)]
        public string Insurance { get; set; }
        [Key(12)]
        public string SSN { get; set; }
        [Key(13)]
        public string FacilityOrHospital { get; set; }
        [Key(14)]
        public string AdmitDate { get; set; }
        [Key(15)]
        public string DischargeDate { get; set; }
        [Key(16)]
        public string SubClient { get; set; }
        [Key(17)]
        public string BillableGroup { get; set; }
        [Key(18)]
        public string QueueIndicator { get; set; }
        [Key(19)]
        public string MeditechID { get; set; }
        [Key(20)]

        public string MeditechMarket { get; set; }
        [Key(21)]

        public string Source { get; set; }
        [Key(22)]

        public string CDIWorksheetReviewed { get; set; }
        [Key(23)]

        public string ConcurrentCodingDate { get; set; }
        [Key(24)]

        public string DRG { get; set; }
        [Key(25)]

        public string PhysicianDRG { get; set; }
        [Key(26)]

        public string AdmittingDiagnosis { get; set; }
        [Key(27)]

        public string PrincipleDiagnosis { get; set; }
        [Key(28)]

        public string Disposition { get; set; }
        [Key(29)]

        public string Dispositioncode { get; set; }
        [Key(30)]

        public string PrimaryProcedure { get; set; }
        [Key(31)]

        public string PrimaryProcedureDOS { get; set; }
        [Key(32)]

        public string PCSProceduresCount { get; set; }
        [Key(33)]

        public string Procedurecoded { get; set; }
        [Key(34)]

        public string CPTProceduresCount { get; set; }
        [Key(35)]

        public string PendedPhysicianname { get; set; }
        [Key(36)]

        public string LOS { get; set; }
        [Key(37)]

        public string ICD10CMCount { get; set; }
        [Key(38)]

        public string CCCount { get; set; }
        [Key(39)]

        public string MCCCount { get; set; }
        [Key(40)]

        public string NoofDiagnoses { get; set; }
        [Key(41)]

        public string BufferColumn1 { get; set; }
        [Key(42)]

        public string BufferColumn2 { get; set; }
        [Key(43)]
        public string BufferColumn3 { get; set; }
        [Key(44)]
        public string BufferColumn4 { get; set; }
        [Key(45)]
        public string BufferColumn5 { get; set; }
        [Key(46)]
        public string BufferColumn6 { get; set; }
        [Key(47)]
        public string BufferColumn7 { get; set; }
        [Key(48)]
        public string BufferColumn8 { get; set; }
        [Key(49)]
        public string BufferColumn9 { get; set; }
        [Key(50)]
        public string BufferColumn10 { get; set; }
        [Key(51)]
        public string BufferColumn11 { get; set; }
        [Key(52)]
        public string BufferColumn12 { get; set; }
        [Key(53)]
        public string BufferColumn13 { get; set; }
        [Key(54)]
        public string BufferColumn14 { get; set; }
        [Key(55)]
        public string BufferColumn15 { get; set; }
        [Key(56)]
        public string BufferColumn16 { get; set; }
        [Key(57)]
        public string BufferColumn17 { get; set; }
        [Key(58)]
        public string BufferColumn18 { get; set; }
        [Key(59)]
        public string BufferColumn19 { get; set; }
        [Key(60)]
        public string BufferColumn20 { get; set; }
        [Key(61)]
        public string BufferColumn24 { get; set; }
        [Key(62)]
        public string BufferColumn21 { get; set; }
        [Key(63)]
        public string BufferColumn22 { get; set; }
        [Key(64)]
        public string BufferColumn23 { get; set; }
        [Key(65)]
        public string BufferColumn25 { get; set; }
        [Key(66)]
        public string BufferColumn26 { get; set; }
        [Key(67)]
        public string BufferColumn27 { get; set; }
        [Key(68)]
        public string BufferColumn28 { get; set; }
        [Key(69)]
        public string BufferColumn29 { get; set; }
        [Key(70)]
        public string BufferColumn30 { get; set; }
        [Key(71)]
        public string BufferColumn31 { get; set; }
        [Key(72)]
        public string BufferColumn32 { get; set; }
        [Key(73)]
        public string BufferColumn33 { get; set; }
        [Key(74)]
        public string BufferColumn34 { get; set; }
        [Key(75)]
        public string BufferColumn35 { get; set; }
        [Key(76)]
        public string BufferColumn36 { get; set; }
        [Key(77)]
        public string BufferColumn37 { get; set; }
        [Key(78)]
        public string BufferColumn38 { get; set; }
        [Key(79)]
        public int? SubClientID { get; set; }
        [Key(80)]
        public int? BillableGroupID { get; set; }
        [Key(81)]
        public int? AssignedTo { get; set; }
        [Key(82)]
        public int? UpdatedBy { get; set; }
        [Key(83)]
        public DateTime? UpdatedDateTimeEST { get; set; }
        [Key(84)]
        public DateTime? UpdatedDateTimeIST { get; set; }
        [Key(85)]
        public DateTime? UpdatedDateTimeGMT { get; set; }
        [Key(86)]
        public int? LastWorkedBy { get; set; }
        [Key(87)]
        public string PendingReasons { get; set; }
    }


}