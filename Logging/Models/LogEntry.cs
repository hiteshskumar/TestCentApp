using System;
using MessagePack;
using Microsoft.Extensions.Logging;

namespace ChargesWFM.UI.Logging.Models
{
    [MessagePackObject]
    public class LogEntry
    {
        [Key(0)]
        public LogLevel Level { get; set; }
        [Key(1)]
        public int EventId { get; set; }
        [Key(2)]
        public string EventName { get; set; }
        [Key(3)]
        public string Category { get; set; }
        [Key(4)]
        public string Message { get; set; }
        [Key(5)]
        public string FormattedMessage { get; set; }
        [Key(6)]
        public string Facility { get; set; }
        [Key(7)]
        public string Version { get; set; }
        [Key(8)]
        public string Host { get; set; }
    }
}