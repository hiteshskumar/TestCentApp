using System;
using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class QAAgentSummaryResponse
    {
        [Key(0)]
        public string Auditor { get; set; }

        [Key(1)]
        public string ProductionAgent { get; set; }

        [Key(2)]
        public string Reviewer { get; set; }

        [Key(3)]
        public string Auditee { get; set; }

        [Key(4)]
        public int TotalQCd { get; set; }

        [Key(5)]
        public int TotalQCReworked { get; set; }
    }

    public class QAAgentSummaryRequest
    {
        [Key(0)]
        public int ProjectgroupID { get; set; }
        [Key(1)]
        public DateTime FromDate { get; set; } = DateTime.Now;

        [Key(2)]
        public DateTime ToDate { get; set; } = DateTime.Now;
        [Key(3)]
        public int AuditLevel { get; set; } = 1;
    }

}