using System.Collections.Generic;
using MessagePack;

#pragma warning disable CS1591
namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class UploadFiles
    {
        [Key(0)]
        public IEnumerable<string> Files { get; set; }

        [Key(1)]
        public bool CanSystemResolve { get; set; }

        [Key(2)]
        public bool UnassignAccounts { get; set; }

        [Key(3)]
        public int PlacementID { get; set; }

        [Key(4)]
        public int ProjectGroupID { get; set; }

        [Key(5)]
        public string UploadedBy { get; set; }

        [Key(6)]
        public int UploadedById { get; set; }
    }
}