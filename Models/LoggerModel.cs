using MessagePack;

namespace ChargesWFM.UI.Models
{

    [MessagePackObject]
    public class LogModelreturn
    {
        [Key(0)]
        public string result { get; set; }
    }

    [MessagePackObject]
    public class LogModel
    {
        [Key(0)]
        public string logLevel { get; set; }
        [Key(1)]
        public string exception { get; set; }
        [Key(2)]
        public string eventId { get; set; }
        [Key(3)]
        public string categoryName { get; set; }
    }

}