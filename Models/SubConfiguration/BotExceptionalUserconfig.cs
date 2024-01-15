using System;
using MessagePack;
using MessagePackKey = MessagePack.KeyAttribute;

#pragma warning disable CS1591
namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class BotExceptionalUserData
    {
        [MessagePackKey(0)]
        public int AuditorID { get; set; }

        [MessagePackKey(1)]
        public string AuditorSigninName { get; set; } = null!;

        [MessagePackKey(2)]
        public string AuditorEmployeeCode { get; set; } = null!;
    }

    [MessagePackObject]
    public class AuditorList
    {
        [MessagePackKey(0)]
        public int EmployeeID { get; set; }

        [MessagePackKey(1)]
        public string EmployeeCode { get; set; } = null!;

        [MessagePackKey(2)]
        public string SigninName { get; set; } = null!;
    }

    [MessagePackObject]
    public class SaveExceptionalUserList
    {
        [MessagePackKey(0)]
        public int ProjectGroupID { get; set; }

        [MessagePackKey(1)]
        public int EmployeeID { get; set; }

        [MessagePackKey(2)]
        public List<AuditorIDList> ExceptionalAuditors { get; set; } = null!;
    }

    [MessagePackObject]
    public class AuditorIDList
    {
        [MessagePackKey(0)]
        public int AuditorID { get; set; }
    }

    [MessagePackObject]
    public class GetBotExceptionalUserListRequest
    {
        [MessagePackKey(0)]
        public int ProjectGroupID { get; set; }

        [MessagePackKey(1)]
        public List<UserNameList> ExceptionUserList { get; set; } = null!;
    }

    [MessagePackObject]
    public class UserNameList
    {
        [MessagePackKey(0)]
        public string AuditorName { get; set; } = string.Empty;
    }

    [MessagePackObject]
    public class UserUploadStatus
    {
        [MessagePackKey(0)]
        public string? AuditorName { get; set; }

        [MessagePackKey(1)]
        public string? EmployeeCode { get; set; }
        [MessagePackKey(1)]
        public string? Status { get; set; }
    }
    
}