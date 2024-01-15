using MessagePack;

#pragma warning disable CS1591
namespace ChargesWFM.UI.Models
{
    // [MessagePackObject]
    // public class UploadFileDetails
    // {
    //     [Key(0)]
    //     public string FilePath { get; set; }

    //     [Key(1)]
    //     public int DatabaseID { get; set; }

    //     [Key(2)]
    //     public int ProjectGroupID { get; set; }

    //     [Key(3)]
    //     public int PlacementID { get; set; }
    // }
        [MessagePackObject]
        public class UploadFileDetails
        {
            [Key(0)]
            public string Name { get; set; }

            [Key(1)]
            public long Size { get; set; }

            [Key(2)]
            public byte[] Bytes { get; set; }
        }
}