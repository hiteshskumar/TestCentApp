using System;
using MessagePack;
using MessagePackKey = MessagePack.KeyAttribute;

#pragma warning disable CS1591
namespace ChargesWFM.UI.Models
{
     [MessagePackObject]

      public class QCHourlyProductivity
    {
        [MessagePackKey(0)]
        public int EmployeeID { get; set; }
        [MessagePackKey(1)]
        public string Employee { get; set; }
        [MessagePackKey(2)]
        public int Total { get; set; }
        [MessagePackKey(3)]
        public int ST0 { get; set; }
        [MessagePackKey(4)]
        public int ST1 { get; set; }
        [MessagePackKey(5)]
        public int ST2 { get; set; }
        [MessagePackKey(6)]
        public int ST3 { get; set; }
        [MessagePackKey(7)]
        public int ST4 { get; set; }
        [MessagePackKey(8)]
        public int ST5 { get; set; }
        [MessagePackKey(9)]
        public int ST6 { get; set; }
        [MessagePackKey(10)]
        public int ST7 { get; set; }
        [MessagePackKey(11)]
        public int ST8 { get; set; }
        [MessagePackKey(12)]
        public int ST9 { get; set; }
        [MessagePackKey(13)]
        public int ST10 { get; set; }
        [MessagePackKey(14)]
        public int ST11 { get; set; }
        [MessagePackKey(15)]
        public int ST12 { get; set; }
        [MessagePackKey(16)]
        public int ST13 { get; set; }
        [MessagePackKey(17)]
        public int ST14 { get; set; }
        [MessagePackKey(18)]
        public int ST15 { get; set; }
        [MessagePackKey(19)]
        public int ST16 { get; set; }
        [MessagePackKey(20)]
        public int ST17 { get; set; }
        [MessagePackKey(21)]
        public int ST18 { get; set; }
        [MessagePackKey(22)]
        public int ST19 { get; set; }
        [MessagePackKey(23)]
        public int ST20 { get; set; }
        [MessagePackKey(24)]
        public int ST21 { get; set; }
        [MessagePackKey(25)]
        public int ST22 { get; set; }
        [MessagePackKey(26)]
        public int ST23 { get; set; }

    }
    
      
}