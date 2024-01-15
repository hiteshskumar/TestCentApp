using System;
using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
    public class GetProductivityReportResults
    {
        [Key(0)]
        public IEnumerable<ResultSet> ResultSet { get; set; }

        [Key(1)]
        public List<String> ColumnSet { get; set; }

        [Key(2)]
        public List<String> DisplayColumn { get; set; }
    }


    [MessagePackObject]
    public class ResultSet
    {
        [Key(0)]
        public string? ID { get; set; }
        [Key(1)]
        public string? AccountTransactionID { get; set; }
        [Key(2)]
        public string? AccountDetailID { get; set; }
        [Key(3)]
        public string? Software { get; set; }

        [Key(4)]
        public string? BillableGroup { get; set; }

        [Key(5)]
        public string? Task { get; set; }

        [Key(6)]
        public string? SubTask { get; set; }

        [Key(7)]
        public string? SubClient { get; set; }

        [Key(8)]
        public string? ClientLogin { get; set; }

        [Key(9)]
        public string? Column1 { get; set; }

        [Key(10)]
        public string? Column2 { get; set; }

        [Key(11)]
        public string? Column3 { get; set; }
        [Key(12)]
        public string? Column4 { get; set; }
        [Key(13)]
        public string? Column5 { get; set; }
        [Key(14)]
        public string? Column6 { get; set; }
        [Key(15)]
        public string? Column7 { get; set; }
        [Key(16)]
        public string? Column8 { get; set; }
        [Key(17)]
        public string? Column9 { get; set; }
        [Key(18)]
        public string? Column10 { get; set; }
        [Key(19)]
        public string? Column11 { get; set; }
        [Key(20)]
        public string? Column12 { get; set; }
        [Key(21)]
        public string? Column13 { get; set; }
        [Key(22)]
        public string? Column14 { get; set; }
        [Key(23)]
        public string? Column15 { get; set; }
        [Key(24)]
        public string? Column16 { get; set; }
        [Key(25)]
        public string? Column17 { get; set; }
        [Key(26)]
        public string? Column18 { get; set; }
        [Key(27)]
        public string? Column19 { get; set; }
        [Key(28)]
        public string? Column20 { get; set; }
        [Key(29)]
        public string? Column21 { get; set; }
        [Key(30)]
        public string? Column22 { get; set; }
        [Key(31)]
        public string? Column23 { get; set; }
        [Key(32)]
        public string? Column24 { get; set; }
        [Key(33)]
        public string? Column25 { get; set; }
        [Key(34)]
        public string? Column26 { get; set; }
        [Key(35)]
        public string? Column27 { get; set; }
        [Key(36)]
        public string? Column28 { get; set; }
        [Key(37)]
        public string? Column29 { get; set; }
        [Key(38)]
        public string? Column30 { get; set; }
        [Key(39)]
        public string? Column31 { get; set; }
        [Key(40)]
        public string? Column32 { get; set; }
        [Key(41)]
        public string? Column33 { get; set; }
        [Key(42)]
        public string? Column34 { get; set; }
        [Key(43)]
        public string? Column35 { get; set; }
        [Key(44)]
        public string? Column36 { get; set; }
        [Key(45)]
        public string? Column37 { get; set; }
        [Key(46)]
        public string? Column38 { get; set; }
        [Key(47)]
        public string? Column39 { get; set; }
        [Key(48)]
        public string? Column40 { get; set; }
        [Key(49)]
        public string? Column41 { get; set; }
        [Key(50)]
        public string? Column42 { get; set; }
        [Key(51)]
        public string? Column43 { get; set; }
        [Key(52)]
        public string? Column44 { get; set; }
        [Key(53)]
        public string? Column45 { get; set; }
        [Key(54)]
        public string? Column46 { get; set; }
        [Key(55)]
        public string? Column47 { get; set; }
        [Key(56)]
        public string? Column48 { get; set; }
        [Key(57)]
        public string? Column49 { get; set; }
        [Key(58)]
        public string? Column50 { get; set; }
        [Key(59)]
        public string? Column51 { get; set; }
        [Key(60)]
        public string? Column52 { get; set; }
        [Key(61)]
        public string? Column53 { get; set; }
        [Key(62)]
        public string? Column54 { get; set; }
        [Key(63)]
        public string? Column55 { get; set; }
        [Key(64)]
        public string? Column56 { get; set; }
        [Key(65)]
        public string? Column57 { get; set; }
        [Key(66)]
        public string? Column58 { get; set; }
        [Key(67)]
        public string? Column59 { get; set; }
        [Key(68)]
        public string? Column60 { get; set; }
        [Key(69)]
        public string? Column61 { get; set; }
        [Key(70)]
        public string? Column62 { get; set; }
        [Key(71)]
        public string? Column63 { get; set; }
        [Key(72)]
        public string? Column64 { get; set; }
        [Key(73)]
        public string? Column65 { get; set; }
        [Key(74)]
        public string? Column66 { get; set; }
        [Key(75)]
        public string? Column67 { get; set; }
        [Key(76)]
        public string? Column68 { get; set; }
        [Key(77)]
        public string? Column69 { get; set; }
        [Key(78)]
        public string? Column70 { get; set; }
        [Key(79)]
        public string? Column71 { get; set; }
        [Key(80)]
        public string? Column72 { get; set; }
        [Key(81)]
        public string? Column73 { get; set; }
        [Key(82)]
        public string? Column74 { get; set; }
        [Key(83)]
        public string? Column75 { get; set; }
        [Key(84)]
        public string? Column76 { get; set; }
        [Key(85)]
        public string? Column77 { get; set; }
        [Key(86)]
        public string? Column78 { get; set; }
        [Key(87)]
        public string? Column79 { get; set; }
        [Key(88)]
        public string? Column80 { get; set; }
        [Key(89)]
        public string? Column81 { get; set; }
        [Key(90)]
        public string? Column82 { get; set; }
        [Key(91)]
        public string? Column83 { get; set; }
        [Key(92)]
        public string? Column84 { get; set; }
        [Key(93)]
        public string? Column85 { get; set; }
        [Key(94)]
        public string? Column86 { get; set; }
        [Key(95)]
        public string? Column87 { get; set; }
        [Key(96)]
        public string? Column88 { get; set; }
        [Key(97)]
        public string? Column89 { get; set; }
        [Key(98)]
        public string? Column90 { get; set; }
        [Key(99)]
        public string? Column91 { get; set; }
        [Key(100)]
        public string? Column92 { get; set; }
        [Key(101)]
        public string? Column93 { get; set; }
        [Key(102)]
        public string? Column94 { get; set; }
        [Key(103)]
        public string? Column95 { get; set; }
        [Key(104)]
        public string? Column96 { get; set; }
        [Key(105)]
        public string? Column97 { get; set; }
        [Key(106)]
        public string? Column98 { get; set; }
        [Key(107)]
        public string? Column99 { get; set; }
        [Key(108)]
        public string? Column100 { get; set; }
        [Key(109)]
        public string? FieldType1 { get; set; }
        [Key(110)]
        public string? FieldType2 { get; set; }
        [Key(111)]
        public string? FieldType3 { get; set; }
        [Key(112)]
        public string? FieldType4 { get; set; }
        [Key(113)]
        public string? FieldType5 { get; set; }
        [Key(114)]
        public string? FieldType6 { get; set; }
        [Key(115)]
        public string? FieldType7 { get; set; }
        [Key(116)]
        public string? FieldType8 { get; set; }
        [Key(117)]
        public string? FieldType9 { get; set; }
        [Key(118)]
        public string? FieldType10 { get; set; }
        [Key(119)]
        public string? FieldType11 { get; set; }
        [Key(120)]
        public string? FieldType12 { get; set; }
        [Key(121)]
        public string? FieldType13 { get; set; }
        [Key(122)]
        public string? FieldType14 { get; set; }
        [Key(123)]
        public string? RiskLevel { get; set; }
        [Key(124)]
        public string? updatedby { get; set; }
        [Key(125)]
        public string? UpdatedDate { get; set; }
        [Key(126)]
        public string? DName { get; set; }
        [Key(127)]
        public string? FName { get; set; }
    }
}