using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class ClientSubClient
    {
        [Key(0)]
        public int ProcessClientID { get; set; }
        [Key(1)]
        public int ClientID { get; set; }
        [Key(2)]
        public string Client { get; set; }
        [Key(3)]
        public int SubClientID { get; set; }
        [Key(4)]
        public string SubClient { get; set; }
        [Key(5)]
        public int ProcessID { get; set; }
        [Key(6)]
        public string Process { get; set; }
        [Key(7)]
        public int BillableGroupID { get; set; }
        [Key(8)]
        public string BillableGroup { get; set; }
        [Key(9)]
        public int DepartmentID { get; set; }
        [Key(10)]
        public string Department { get; set; }
        [Key(11)]
        public int HugoProjectID { get; set; }
    }
}