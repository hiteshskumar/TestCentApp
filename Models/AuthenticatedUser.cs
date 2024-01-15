using System.Collections.Generic;
using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class AuthenticatedUser
    {
        [Key(0)]
        public int EmployeeId { get; set; }
        [Key(1)]
        public string SigninName { get; set; }
        [Key(2)]
        public string FirstName { get; set; }
        [Key(3)]
        public string LastName { get; set; }
        [Key(4)]
        public IEnumerable<Role> Roles { get; set; }
        [Key(5)]
        public IEnumerable<Screen> Screens { get; set; }
        [Key(6)]
        public string EncryptedKey { get; set; }
        // [Key(7)]
        // public IEnumerable<ProcessClientProjectGroupMapping> ProcessClientProjectGroupMappings { get; set; }
        [Key(7)]
        public int ProjectGroupID {get; set;}
    }
}