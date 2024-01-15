using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class StatusMessageResponse
    {
        [Key(0)]
        public string Status { get; set; } = null!;
        [Key(1)]
        public int StatusID { get; set; }
    }
    [MessagePackObject]
    public class ElasticSearchResponse
    {
        [Key(0)]
        public string Keys { get; set; }
        [Key(1)]
        public string Values { get; set; }
    }
}