using System;
using MessagePack;

namespace ChargesWFM.UI.Models
{   
    [MessagePackObject]   
    public class EmployeeCategoryMappings
    {
        [Key(0)]
        public int ReportingEmployeeID { get; set; }
        [Key(1)]
        public string? ReportingEmployeeLoginName { get; set; }
        [Key(2)]
        public string? LoginTime { get; set; }
        [Key(3)]
        public string? LogoutTime { get; set; }
        [Key(4)]
        public int EmployeeCategory { get; set; }
        [Key(5)]
        public int LoginStatus { get; set; }
        [Key(6)]
        public string? ReportingAuthorityName { get; set; }
        [Key(7)]
        public bool IsChecked { get; set; } = false;
    }
    [MessagePackObject]
    public class UpdateEmployeeCategoryMappingDetails
    {
        [Key(0)]
        public IEnumerable<EmployeeCategoryMappings>? EmployeeCategoryMappingDetails { get; set; }
        [Key(1)]
        public long ProjectGroupID { get; set; }
        [Key(2)]
        public long EmployeeID { get; set; }
    }
    [MessagePackObject]
    public class DropDownList
    {
        [Key(0)]
        public int ID { get; set; }
        [Key(1)]
        public string? Name { get; set; }
    }
    [MessagePackObject]   
    public class EmployeeCategoryMappingTemplate
    {       
        [Key(0)]
        public string? EmployeeName { get; set; }
        [Key(1)]
        public string? EmployeeCategory { get; set; }
    }
}