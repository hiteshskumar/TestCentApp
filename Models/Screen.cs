using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class Screen
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public string Name { get; set; }
    }
}