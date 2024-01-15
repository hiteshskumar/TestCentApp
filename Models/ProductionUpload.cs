using MessagePack;
using System;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
     public class ProductionUploadTemplate
     {
        [Key(0)]
        public string? DisplayName { get; set; }
        [Key(1)]
        public int DisplayOrder { get; set; }
     }
   
    [MessagePackObject]
    public class SkipReasonModel
    {
        [Key(0)]
        public int ProjectGroupID { get; set; }
        [Key(1)]
        public int UploadID { get; set; }
        [Key(2)]
        public int EmployeeID { get; set; }
        [Key(3)]
        public string SchemaName { get; set; }
    }
}
   