using MessagePack;
using MessagePackKey = MessagePack.KeyAttribute;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class AuditeeToReviewerMappingDetails
    {
        [MessagePackKey(0)]
        public int PoolID { get; set; }
        [MessagePackKey(1)]
        public string PoolName { get; set; }
        [MessagePackKey(2)]
        public string PoolType { get; set; }
        [MessagePackKey(3)]
        public bool IsSystemPool { get; set; }
        [MessagePackKey(4)]
        public string AuditLevel { get; set; }
        [MessagePackKey(5)]
        public int ProjectGroupID { get; set; }
        [MessagePackKey(6)]
        public int TotalAuditee { get; set; }
        [MessagePackKey(7)]
        public int AuditeeID { get; set; }
        [MessagePackKey(8)]
        public string AuditeeCode { get; set; }
        [MessagePackKey(9)]
        public string AuditeeName { get; set; }
        [MessagePackKey(10)]
        public int TotalPrimaryReviewer { get; set; }
        [MessagePackKey(11)]
        public int PrimaryReviewerID { get; set; }
        [MessagePackKey(12)]
        public string PrimaryReviewerCode { get; set; }
        [MessagePackKey(13)]
        public string PrimaryReviewerName { get; set; }
        [MessagePackKey(14)]
        public int TotalSecondaryReviewer { get; set; }
        [MessagePackKey(15)]
        public int SecondaryReviewerID { get; set; }
        [MessagePackKey(16)]
        public string SecondaryReviewerCode { get; set; }
        [MessagePackKey(17)]
        public string SecondaryReviewerName { get; set; }
        [MessagePackKey(18)]
        public bool IsPoolAlreadyMapped { get; set; }
        [MessagePackKey(19)]
        public string MappedRuleNameList { get; set; }
    }

    [MessagePackObject]
    public class AuditeePooling
    {
        [MessagePackKey(0)]
        public int PoolID { get; set; }
        [MessagePackKey(1)]
        public string PoolName { get; set; }
        [MessagePackKey(2)]
        public int AuditeeID { get; set; }
        [MessagePackKey(3)]
        public string AuditeeCode { get; set; }
        [MessagePackKey(4)]
        public string AuditeeName { get; set; }
        [MessagePackKey(5)]
        public int UpdatedBy { get; set; }
        [MessagePackKey(6)]
        public DateTime UpdatedOnIST { get; set; }
    }

    [MessagePackObject]
    public class ReviewerPooling
    {
        [MessagePackKey(0)]
        public int PoolID { get; set; }
        [MessagePackKey(1)]
        public string PoolName { get; set; }
        [MessagePackKey(2)]
        public int ReviewerID { get; set; }
        [MessagePackKey(3)]
        public string ReviewerCode { get; set; }
        [MessagePackKey(4)]
        public string ReviewerName { get; set; }
        [MessagePackKey(5)]
        public string ReviewerType { get; set; }
        [MessagePackKey(6)]
        public int UpdatedBy { get; set; }
        [MessagePackKey(7)]
        public DateTime UpdatedOnIST { get; set; }
    }

    [MessagePackObject]
    public class AuditeeList
    {
        [Key(0)]
        public int AuditeeID { get; set; }
        [Key(1)]
        public string AuditeeCode { get; set; }
        [Key(2)]
        public string AuditeeName { get; set; }
        [Key(3)]
        public string AuditeeStatus { get; set; }
    }

    [MessagePackObject]
    public class ReviewerList
    {
        [Key(0)]
        public int ReviewerID { get; set; }
        [Key(1)]
        public string ReviewerCode { get; set; }
        [Key(2)]
        public string ReviewerName { get; set; }
    }

    [MessagePackObject]
    public class Auditee
    {
        [MessagePackKey(0)]
        public int AuditeeID { get; set; }
        [MessagePackKey(1)]
        public string AuditeeCode { get; set; }
        [MessagePackKey(2)]
        public string AuditeeName { get; set; }
    }

    [MessagePackObject]
    public class Reviewer
    {
        [MessagePackKey(0)]
        public int ReviewerID { get; set; }
        [MessagePackKey(1)]
        public string ReviewerCode { get; set; }
        [MessagePackKey(2)]
        public string ReviewerName { get; set; }
    }
}