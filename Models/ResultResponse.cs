using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class ResultResponse
    {
        [Key(0)]
        public string ResultMessage { get; set; }

        [Key(1)]
        public int ResultCode { get; set; }

        [Key(2)]
        public IEnumerable<Tuple<bool, string>> BusinessRuleMessage { get; set; }
    }
    [MessagePackObject]
    public class BulkCompletion
    {
        [Key(0)]
        public int EmployeeId { get; set; }
        [Key(1)]
        public string? SchemaName { get; set; }
        [Key(2)]
        public int status { get; set; }
        [Key(3)]
        public string? BulkUpdateUploadIdList { get; set; }
        [Key(4)]
        public string? Comments { get; set; }
        [Key(5)]
        public int ProjectGroupID { get; set; }
        [Key(6)]
        public string? ClientDisposition { get; set; }
        [Key(7)]
        public string? InsCat { get; set; }
        [Key(8)]
        public string? InsPhoneNumber { get; set; }
    }
}