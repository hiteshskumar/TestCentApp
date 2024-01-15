using System;
using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class PGAuditforms
    {
        [Key(0)]
        public int AuditFormID { get; set; }
        [Key(1)]
        public string AuditFormName { get; set; }
    }

    [MessagePackObject]
    public class RebuttalSummaryRequest
    {

        [Key(0)]
        public int AuditFormID { get; set; }

        [Key(1)]
        public DateOnly FromDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Key(2)]

        public DateOnly ToDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [Key(3)]
        public string AuditStatus { get; set; }
        [Key(4)]

        public int AuditLevel { get; set; }
    }
    

    [MessagePackObject]
    public class RebuttalDetails
    {
        [Key(0)]
        public string EmployeeName { get; set; }
        [Key(1)]
        public int? Auditee_Open { get; set; }
        [Key(2)]
        public int? AuditeeTL_Open { get; set; }
        [Key(3)]
        public int? Reviewer_Open { get; set; }
        [Key(4)]
        public int? ReviewerTL_Open { get; set; }
        [Key(5)]
        public int? Auditee_Closed { get; set; }
        [Key(6)]
        public int? AuditeeTL_Closed { get; set; }
        [Key(7)]
        public int? Reviewer_Closed { get; set; }
        [Key(8)]
        public int? ReviewerTL_Closed { get; set; }
        [Key(9)]
        public int? Auditee_AutoClosed { get; set; }
        [Key(10)]
        public int? AuditeeTL_AutoClosed { get; set; }
        [Key(11)]
        public int? Reviewer_AutoClosed { get; set; }
        [Key(12)]
        public int? ReviewerTL_AutoClosed { get; set; }
        [Key(13)]
        public int? Auditee_FirstLevelReviewer_Open { get; set; }
        [Key(14)]
        public int? AuditeeTL_FirstLevelReviewerTL_Open { get; set; }
        [Key(15)]
        public int? Reviewer_secondLevelReviewer_Open { get; set; }
        [Key(16)]
        public int? ReviewerTL_secondLevelReviewerTL_Open { get; set; }
        [Key(17)]
        public int? Auditee_FirstLevelReviewer_Closed { get; set; }
        [Key(18)]
        public int? AuditeeTL_FirstLevelReviewerTL_Closed { get; set; }
        [Key(19)]
        public int? Reviewer_secondLevelReviewer_Closed { get; set; }
        [Key(20)]
        public int? ReviewerTL_secondLevelReviewerTL_Closed { get; set; }
        [Key(21)]
        public int? Auditee_FirstLevelReviewer_AutoClosed { get; set; }
        [Key(22)]
        public int? AuditeeTL_FirstLevelReviewerTL_AutoClosed { get; set; }
        [Key(23)]
        public int? Reviewer_secondLevelReviewer_AutoClosed { get; set; }
        [Key(24)]
        public int? ReviewerTL_secondLevelReviewerTL_AutoClosed { get; set; }
        [Key(25)]
        public int? Auditee_SecondLevelReviewer_Open { get; set; }
        [Key(26)]
        public int? AuditeeTL_SecondLevelReviewerTL_Open { get; set; }
        [Key(27)]
        public int? Reviewer_ThirdLevelReviewer_Open { get; set; }
        [Key(28)]
        public int? ReviewerTL_ThirdLevelReviewerTL_Open { get; set; }
        [Key(29)]
        public int? Auditee_SecondLevelReviewer_Closed { get; set; }
        [Key(30)]
        public int? AuditeeTL_SecondLevelReviewerTL_Closed { get; set; }
        [Key(31)]
        public int? Reviewer_ThirdLevelReviewer_Closed { get; set; }
        [Key(32)]
        public int? ReviewerTL_ThirdLevelReviewerTL_Closed { get; set; }
        [Key(33)]
        public int? Auditee_SecondLevelReviewer_AutoClosed { get; set; }
        [Key(34)]
        public int? AuditeeTL_SecondLevelReviewerTL_AutoClosed { get; set; }
        [Key(35)]
        public int? Reviewer_ThirdLevelReviewer_AutoClosed { get; set; }
        [Key(36)]
        public int? ReviewerTL_ThirdLevelReviewerTL_AutoClosed { get; set; }

    }

}