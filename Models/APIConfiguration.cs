using System;
using MessagePack;

namespace ChargesWFM.UI.Models
{
    [MessagePackObject]
     public class ProcessClients
     {
        [Key(0)]
        public string? Process { get; set; }
        [Key(1)]
        public string? Client { get; set; }
        [Key(2)]
        public int DepartmentID { get; set; }
        [Key(3)]
        public int ProcessID { get; set; }
        [Key(4)]
        public int ClientID { get; set; }
     }
      [MessagePackObject]
    public class NexgenProjectGroups
    {
       
        [Key(0)]
        public int ProjectGroupID { get; set; }
        [Key(1)]
        public string? ProjectGroupName { get; set; }
        [Key(2)]
        public string? ProjectGroupCode { get; set; }
        [Key(3)]
        public int DatabaseID { get; set; }
        [Key(4)]
        public string? Database { get; set; }
        [Key(5)]
        public string? SchemaName { get; set; }
        [Key(6)]
        public string? ServerName { get; set; }
    }

    [MessagePackObject]
    public class TimeSheetData
    {
        [Key(0)]
        public int? SubClientID { get; set; }
        [Key(1)]
        public string? SubClient { get; set; }
        [Key(2)]
        public int? BillableGroupID { get; set; }
        [Key(3)]
        public string? BillableGroup { get; set; }
        [Key(4)]
        public int? SoftwareID { get; set; }
        [Key(5)]
        public string? Software { get; set; }
        [Key(6)]
        public int? ClientLoginID { get; set; }
        [Key(7)]
        public string? ClientLogin { get; set; }
        [Key(8)]
        public int? TaskID { get; set; }
        [Key(9)]
        public string? Task { get; set; }
        [Key(10)]
        public int? SubTaskID { get; set; }
        [Key(11)]
        public string? SubTask { get; set; }
        [Key(12)]
        public Decimal? InternalTarget { get; set; }
    }

    [MessagePackObject]
    public class ElasticSearchApiURL
    {
        [Key(0)]
        public  string? EnvironmentProtocol { get; set; }
        [Key(1)]
        public string? EnvironmentPort { get; set; }
        [Key(2)]
        public string? IndexName { get; set; }
        [Key(3)]
        public string? TypeName { get; set; }
    }

    [MessagePackObject]
    public class APIConfiguration {
        [Key(0)]
        public int ApiID { get; set; }
        [Key(1)]
        public string ApiName { get; set; }
        [Key(2)]
        public string ApiUrl { get; set; }
        [Key(3)]
        public string ApiDescriptions { get; set; }
        [Key(4)]
        public string SearchColumns { get; set; }
        [Key(5)]
        public string FetchColumns { get; set; }
        [Key(6)]
        public string SelectValue { get; set; }
        [Key(7)]
        public string DisplayFormat { get; set; }
        [Key(8)]
        public string? FilterParameter1 { get; set; }
        [Key(9)]
        public string? FilterParameter2 { get; set; }
        [Key(10)]
        public string? FilterParameter3 { get; set; }
        [Key(11)]
        public int? CreatedBy { get; set; }
        [Key(12)]
        public DateTime? CreatedOnIST { get; set; }
        [Key(13)]
        public DateTime? UpdatedOnIST { get; set; }
        [Key(14)]
        public int? UpdatedBy { get; set; }

        public void clear() {
            ApiID = 0;
            ApiName = string.Empty;
            ApiDescriptions = string.Empty;
            ApiUrl = string.Empty;
            SearchColumns = string.Empty;
            FetchColumns = string.Empty;
            SelectValue = string.Empty;
            DisplayFormat = string.Empty;
            FilterParameter1 = string.Empty;
            FilterParameter2 = string.Empty;
            FilterParameter3 = string.Empty;
        }
    }
    
    public class Paramsdropdowns
    {
        public string label { get; set; }
        public int value { get; set; }
    }
}