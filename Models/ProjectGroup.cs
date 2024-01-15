using MessagePack;
using System;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class ProjectGroup
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public string Name {get; set;}
        [Key(2)]
        public int DatabaseID {get; set;}
        [Key(3)]
        public int ClonedProjectGroupID {get; set;}
        [Key(4)]
        public bool IsActive {get; set;}
        [Key(5)]
        public int CreatedBy {get; set;}
        [Key(6)]
        public DateTime CreatedDateTimeIST{get; set;}
        [Key(7)]
        public int UpdatedBy {get; set;}
        [Key(8)]
        public DateTime UpdatedDateTimeIST {get; set;}
        [Key(9)]
        public string CreatedByEmployee { get; set; }
        [Key(10)]
        public string UpdatedByEmployee { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return $"{Id}{Name}".GetHashCode();
        }
    }
}