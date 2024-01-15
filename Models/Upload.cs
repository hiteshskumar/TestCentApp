using System;
using MessagePack;

namespace ChargesWFM.UI.Models
{
   
    [MessagePackObject]
     public class UploadTemplate
    {
         [Key(0)]
        public int UploadTemplateID { get; set; }
         [Key(1)]
        public string UploadTemplateName { get; set; }
        [Key(2)]
        public bool AllocateToPreviouslyWorkedAgents { get; set; }
         [Key(3)]
        public bool AllocateToIdleAgents { get; set; }
        [Key(4)]
        public string AllocationType { get; set; }
        [Key(5)]
        public string AllocationMethod { get; set; }
        [Key(6)]
        public bool AppendUpload { get; set; }
        [Key(7)]
        public bool AllocateToSkipAccounts { get; set; }
        [Key(8)]
        public bool Skip { get; set; }
        [Key(9)]
        public bool IsWorkableFilter { get; set; }
        [Key(10)]
        public int WorkableFlag { get; set; }
        [Key(11)]
        public bool SearchandEdit { get; set; }
    }
    [MessagePackObject]
    public class UploadSuccessConfirmation
    {
        [Key(0)]
        public IEnumerable<ErrorDetail> ErrorDetail { get; set; }
        [Key(1)]
        public IEnumerable<SuccessConfirmation> Confirmation { get; set; }

    }
    [MessagePackObject]
    public class ErrorDetail
    {
        [Key(0)]
        public string Error { get; set; }
        [Key(1)]
        public string Description { get; set; }
        [Key(2)]
        public int RowNumber { get; set; }
        [Key(3)]
        public string? UploadErrorDetails { get; set; }
    }
    
    [MessagePackObject]
    public class SuccessConfirmation
    {
        [Key(0)]
        public string? Status { get; set; }
        [Key(1)]
        public int StatusCode { get; set; }
        [Key(2)]
        public string? Accounts { get; set; }
        [Key(3)]
        public string? ProjectGroupEmployee { get; set; }
        [Key(4)]
        public int EmployeeId { get; set; }
    }
    [MessagePackObject]
    public class UploadProductionExcelFile
    {
        [Key(0)]
        public string? UploadFileName { get; set; }
        [Key(1)]
        public byte[]? UploadFileBytes { get; set; }
        [Key(2)]
        public long ProjectGroupID { get; set; }
        [Key(3)]
        public long UploadTemplateID { get; set; }
        [Key(4)]
        public long UploadedById { get; set; }
        [Key(5)]
        public string? SchemaName { get; set; }
        [Key(6)]
        public bool ISAppend { get; set; }
    }
}