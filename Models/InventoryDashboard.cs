using System;
using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class GetInventoryInput
    {
        [Key(0)]
        public int EmployeeID { get; set; }
        [Key(1)]
        public int ProjectGroupId { get; set; }
        [Key(2)]
        public string FromDate { get; set; }
        [Key(3)]
        public string ToDate { get; set; }
        [Key(4)]
        public string FromDOS { get; set; }
        [Key(5)]
        public string ToDOS { get; set; }
        [Key(6)]
        public string WorkQueue { get; set; }
        [Key(7)]
        public string QueueName { get; set; }
        [Key(8)]
        public string SchemaName { get; set; }
    }
    
    [MessagePackObject]
    public class GetWorkQueueResults
    {
        [Key(0)]
        public int WorkQueueId { get; set; }
        [Key(1)]
        public string WorkQueueValue { get; set; }
    }

    [MessagePackObject]
    public class GetInventoryMastersResults
    {
        [Key(0)]
        public IEnumerable<GetMasterDetails> QueueName {get; set;}

        [Key(1)]
        public IEnumerable<GetMasterDetails> ClientDisposition {get; set;}

    }

    [MessagePackObject]
    public class GetMasterDetails
    {
        [Key(0)]
        public int MasterID { get; set; }
        [Key(1)]
        public string MasterValue { get; set; }
    }
    [MessagePackObject]
    public class GetInventoryResults
    {
        [Key(0)]
        public IEnumerable<Inventory> Inventory { get; set; }

        [Key(1)]
        public IEnumerable<Summary> Summary { get; set; }
    }

    [MessagePackObject]
    public class Summary
    {
        [Key(0)]
        public int Coded { get; set; }
        [Key(1)]
        public int Pended { get; set; }
        [Key(2)]
        public int InProgress { get; set; }
        [Key(3)]
        public int BackLog { get; set; }
        [Key(4)]
        public int Assigned { get; set; }
        [Key(5)]
        public int Skipped { get; set; }
    }

    [MessagePackObject]
    public class Inventory
    {
        [Key(0)]
        public string Coder { get; set; }
        [Key(1)]
        public int EmployeeID { get; set; }
        [Key(2)]
        public string AchievedPercentageWithFullTarget { get; set; }
        [Key(3)]
        public string AchievedPercentageWithCategoryTarget { get; set; }
        [Key(4)]
        public int Assignedaccounts { get; set; }
        [Key(5)]
        public int Coded { get; set; }
        [Key(6)]
        public int Pended { get; set; }
        [Key(7)]
        public int Skipped { get; set; }
        [Key(8)]
        public int InProgress { get; set; }
        [Key(9)]
        public int Unfetched { get; set; }


    }
}
