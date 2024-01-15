using System;
using MessagePack;
using MessagePackKey = MessagePack.KeyAttribute;

#pragma warning disable CS1591
namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class AverageHoldTimeConfigData : ICloneable<AverageHoldTimeConfigData>
    {
        [MessagePackKey(0)]
        public int AuditFormID { get; set; }
        [MessagePackKey(1)]
        public string AuditFormName { get; set; }
        [MessagePackKey(2)]
        public string BillableGroupID { get; set; }
        [MessagePackKey(3)]
        public string BillableGroup { get; set; }

        [MessagePackKey(4)]
        public Int16 HoldTimeInMinutes { get; set; }
        [MessagePackKey(5)]
        public Int16 SLHoldTimeInMinutes { get; set; }
        [MessagePackKey(6)]
        public Int16 TLHoldTimeInMinutes { get; set; }

        public AverageHoldTimeConfigData Clone()
        {
            return new AverageHoldTimeConfigData
            {
                AuditFormID = this.AuditFormID,
                AuditFormName = this.AuditFormName,
                BillableGroup = this.BillableGroup,
                BillableGroupID = this.BillableGroupID,
                HoldTimeInMinutes = this.HoldTimeInMinutes,
                SLHoldTimeInMinutes = this.SLHoldTimeInMinutes,
                TLHoldTimeInMinutes = this.TLHoldTimeInMinutes
            };
        }
    }

    public class IAuditFormNameData
    {
        public int AuditFormID { get; set; }
        public string AuditFormName { get; set; }
    }

    public class IAuditFormBillablegroupData
    {
        public int AuditFormID { get; set; }
        public string AuditFormName { get; set; }
        public IEnumerable<BillableGroupsDropdown> BillableGroups { get; set; }
    }

    [MessagePackObject]
    public class AuditFormBillablegroupData
    {
        [MessagePackKey(0)]
        public int AuditFormID { get; set; }
        [MessagePackKey(1)]
        public string AuditFormName { get; set; }
        [MessagePackKey(2)]
        public IEnumerable<BillableGroupsDropdown> BillableGroups { get; set; }
        [MessagePackKey(3)]
        public int HourlyCapDuration { get; set; }
    }

    [MessagePackObject]
    public class BillableGroupsDropdown
    {
        [MessagePackKey(0)]
        public int id { get; set; }
        [MessagePackKey(1)]
        public string itemName { get; set; }
        [MessagePackKey(2)]
        public int value { get; set; }
    }
    public class AddOrEditTemplate
    {
        public string PopUpHeaderName { get; set; }
        public string PopUpButtonName { get; set; }
    }


    [MessagePackObject]
    public class PostAverageHoldTimeConfigData
    {
        [MessagePackKey(0)]
        public int ProjectGroupID { get; set; }
        [MessagePackKey(1)]
        public IEnumerable<AverageHoldTimeConfigDataList> AverageholdTimeConfigData { get; set; } = null!;
        [MessagePackKey(2)]
        public int EmployeeID { get; set; }
    }

    [MessagePackObject]
    public class AverageHoldTimeConfigDataList
    {
        [MessagePackKey(0)]
        public int AuditFormID { get; set; }
        [MessagePackKey(1)]
        public string BillableGroupID { get; set; }
        [MessagePackKey(2)]
        public int? AvgHoldTime { get; set; }
        [MessagePackKey(3)]
        public int? SLAvgHoldTime { get; set; }
        [MessagePackKey(4)]
        public int? TLAvgHoldTime { get; set; }
    }

    public interface ICloneable<T>
    {
        T Clone();
    }

}