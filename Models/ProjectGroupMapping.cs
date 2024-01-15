using System.Collections.Generic;
using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class ProjectGroupMapping
    {
        [Key(0)]
        public ProjectGroup ProjectGroup { get; set; }
    }
}