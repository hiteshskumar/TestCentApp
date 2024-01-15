using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class Role
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public string Name { get; set; }
        [Key(2)]
        public bool IsActive { get; set; }
    }
}