using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class UploadProgress
    {
        [Key(0)]
        public bool IsCompleted { get; set; }

        [Key(1)]
        public bool IsFailed { get; set; }

        [Key(2)]
        public string Message { get; set; }

        [Key(3)]
        public string UploadedBy { get; set; }

        [Key(4)]
        public int UploadedById { get; set; }

        [Key(5)]
        public int ProjectGroupId { get; set; }

        [Key(6)]
        public int PlacementId { get; set; }

        [Key(7)]
        public bool IsTemplateError { get; set; }

        [Key(8)]
        public string FilePathName { get; set; }
    }
}