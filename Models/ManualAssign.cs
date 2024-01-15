using System;
using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]

    public class MAssign
    {
        [Key(0)]
        public string AllocationType {get; set;}

        [Key(1)]
        public string AllocationMethod {get; set;}
        
        [Key(2)]
        public int AllocateToPreviouslyWorkedAgents {get; set;}

        [Key(3)]
        public int AllocateToIdleAgents {get; set;}

        [Key(4)]
        public int AllocateMultipleAgents {get; set;}

        [Key(5)]
        public int AppendUpload {get; set;}

        [Key(6)]
        public int Skip {get; set;}

        [Key(7)]
        public int IsWorkableFilter {get; set;}

         [Key(8)]
        public int UploadTemplateID {get; set;}

        [Key(9)]
        public string UploadTemplateName {get; set;}

        [Key(10)]
        public int AllocateToSkipAccounts {get; set;}

        [Key(11)]
        public int ProcessID {get; set;}

        [Key(12)]
        public int SearchandEdit {get; set;}
    }

    [MessagePackObject]

    public class ProcessEmployee{
    [Key(0)]
      public int EmployeeID {get; set;}

      [Key(1)]
      public string EmployeeCode { get; set; }

      [Key(2)]
      public string SigninName { get; set; }
    }

    [MessagePackObject]
    public class DownloadExcelManualAssign
    {
        [Key(0)]
        public IEnumerable<GetFieldResult>? FieldResult { get; set; }

        [Key(1)]
        //public IEnumerable<GetAccountDetailsResult> Accountdetails { get; set; }
        public IEnumerable<GetSearchAndCodeResult>? Accountdetails { get; set; }
    }


    [MessagePackObject]

     public class GetAccountDetailsResult
     {
        [Key(0)]
        public DateTime DOB {get; set;}

        [Key(1)]
        public string MRNNumber {get; set;}

        [Key(2)]
        public string ChartIDOrFileID {get; set;}

        [Key(3)]
        public string Insurance {get; set;}

        [Key(4)]
        public string BufferColumn1 {get; set;}

        [Key(5)]
        public string BufferColumn2 {get; set;}

        [Key(6)]
        public string BufferColumn3 {get; set;}
                   
        [Key(7)]
        public int UploadID {get; set;}

        [Key(8)]
        public bool IsChecked { get; set; }

        [Key(9)]
        public string Assigned {get; set;}

        [Key(10)]
        public string SubClient {get; set;}

        [Key(11)]
        public string BillableGroup {get; set;}
        
        [Key(12)]
        public string Software {get; set;}      
     }

     [MessagePackObject]
    public class GetFieldResult{

        [Key(0)]
        public string? Field {get; set;}

        [Key(1)]
        public string? DisplayName {get; set;}

         [Key(2)]
        public int FieldID    {get; set;}

         [Key(3)]
        public int InputControlID {get; set;}

         [Key(4)]
        public string? BufferFieldType {get; set;}

        [Key(5)]
        public string? DynamicColumns {get; set;}

        [Key(6)]
        public string? InputDataType {get; set;}
    }

    [MessagePackObject]
    public class FieldAccountMapping
    {
        [Key(0)]
        public IEnumerable<GetFieldResult> FieldResult { get; set; }

        // [Key(1)]
        // public IEnumerable<GetAccountDetailsResult> Accountdetails { get; set; }
        [Key(1)]
        public IEnumerable<GetSearchAndCodeResult> Accountdetails { get; set; }
    }

    [MessagePackObject]
    public class UploadAssignedAccountDetails
    {
        [Key(0)]
        public long? UploadID { get; set; }
        [Key(1)]
        public int? PreviousAssignedTo { get; set; }
        [Key(2)]
        public int? CurrentAssignedTo { get; set; }
        [Key(3)]
        public bool? IsFetched { get; set; }
        [Key(4)]
        public int? UpdatedBy { get; set; }
        [Key(5)]
        public DateTime? UpdatedDateTimeEST { get; set; }
        [Key(6)]
        public DateTime? UpdatedDateTimeIST { get; set; }
        [Key(7)]
        public DateTime? UpdatedDateTimeGMT { get; set; }

    }  

     [MessagePackObject]
        public class GetAssignUploadDetails
        {
            [Key(0)]
            public int UploadTemplateID {get; set;}

            [Key(1)]
            public string[]? Accounts {get; set;}

            [Key(2)]
            public int EmployeeID {get; set;}

            [Key(3)]
            public int ProjectGroupEmployee {get; set;}

            [Key(4)]
            public int ProjectGroupID {get; set;}

            [Key(5)]
            public string? SchemaName {get; set;}

            [Key(6)]
            public GetValidationSGBOTGrid[]? BatchDetails {get; set;}
            
        }
    [MessagePackObject]
    public class GetAccount
    {
        [Key(0)]
        public int UploadTemplateID { get; set; }

        [Key(1)]
        public string? Filter { get; set; }

        [Key(2)]
        public int ProjectGroupID { get; set; }

        [Key(3)]
        public string? SchemaName { get; set; }

    }

    [MessagePackObject]
    public class GetValidationSGBOTGrid
    {
        [Key(0)]
        public string? BatchName { get; set; }
        [Key(1)]
        public string? TotalDocuments { get; set; }
        [Key(2)]
        public string? TotalPages { get; set; }
        [Key(3)]
        public string? FetchStatus { get; set; }
        [Key(4)]
        public string? Assigned { get; set; }
        [Key(5)]
        public bool IsBOTChecked { get; set; }
        [Key(6)]
        public bool AssignStatus { get; set; }
    } 
}