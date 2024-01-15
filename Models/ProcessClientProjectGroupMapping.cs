using MessagePack;
using System.Collections.Generic;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class ProcessClientProjectGroupMapping
    {
        [Key(0)]
        public ClientSubClient ClientSubClient { get; set; }
        [Key(1)]
        public IEnumerable<ProjectGroupMapping> ProjectGroupMappings { get; set; }
    }
}