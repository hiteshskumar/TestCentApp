using System;
using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components.Authorization;
using ChargesWFM.UI.Components.GridComponent;
using ChargesWFM.UI.Models;
using ChargesWFM.UI.Services;
using Microsoft.JSInterop;
using MatBlazor;
using System.Data;
using System.Reflection;
using BlazorDownloadFile;
using System.Globalization;

namespace ChargesWFM.UI.Pages.InventoryDashboard
{
    public partial class InventoryDashboard
    {
        [Inject]
        HttpClient? Http { get; set; }
        [Inject]
        AuthenticationStateProvider? AuthProvider { get; set; }
        [Inject]
        ILocalStorageService? LocalStorageService { get; set; }
        [Inject]
        ILogger<InventoryDashboard> Logger { get; set; }
        [Inject]
        IToasterService? ToasterService { get; set; }
        [Inject]
        IMatToaster? Toaster { get; set; }
        [Inject]
        IBlazorDownloadFileService? BlazorDownloadFileService { get; set; }
        private Tuple<string, GridSortOrder>? sortColumn;
        private bool isHide = true;
        private bool isWorkQueueHide = true;

        private bool isQueueNameHide = true;
        private bool isFosHide = true;
        private Inventory[]? _InventoryData;
        private GetInventoryResults[]? _InventorySummary;
        public int EmpID = 0;
        public int PGroupID = 0;
        public string? schemaName;
        private bool displayLoading;
        private string? progressText { get; set; }

        public string? FromDate { get; set; }

        public string? ToDate { get; set; }

        public string? FromDos { get; set; }

        public string? ToDos { get; set; }
        private GetWorkQueueResults[]? dsworkqueue;
        private GetInventoryMastersResults[]? dsInventoryMaster;
        private GetMasterDetails[]? dsQueueName;
        public EventCallback<DateTime?> DateSelected { get; set; }
        private List<string>? selectedWorkQueue { get; set; }
        private List<string>? selectedQueueName { get; set; }

        private List<GetInventoryInput>? GetInventoryInput { get; set; }
        private string WorkQueue, QueueName;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                DisplayLoading(true, "Loading");
                var user = await LocalStorageService.GetAsync<AuthenticatedUser>();
                EmpID = user.EmployeeId;

                var projectGroup = await LocalStorageService.GetAsync<NexgenProjectGroups>();
                PGroupID = projectGroup.ProjectGroupID;
                schemaName = projectGroup.SchemaName;
                isFosHide = true; isQueueNameHide = true; isHide = true; isWorkQueueHide = true;
                if (PGroupID == 6582)
                {
                    isFosHide = false;
                    await GetWorkqueue(PGroupID);
                }

                if (PGroupID == 6632 || PGroupID == 6633)
                {
                    await GetInventoryMasters(PGroupID);
                }
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error occured", MatToastType.Danger, "Inventory Dashboard");
                DisplayLoading(false);
            }
        }

        private void DisplayLoading(bool display, string? progressMessage = null)
        {
            displayLoading = display;
            progressText = progressMessage ?? string.Empty;
            StateHasChanged();
        }
        protected async Task OnProjectGroupChanged(NexgenProjectGroups pg)
        {
            this.PGroupID = pg.ProjectGroupID;
            this.schemaName = pg.SchemaName;
            isFosHide = true; isQueueNameHide = true; isHide = true; isWorkQueueHide = true;
            if (PGroupID == 6582)
            {
                isFosHide = false;
                await GetWorkqueue(PGroupID);
            }
            if (PGroupID == 6632 || PGroupID == 6633)
            {
                await GetInventoryMasters(PGroupID);
            }
            this.StateHasChanged();
        }


        private Task OnFromDateSelected(ChangeEventArgs args)
        {
            var text = args.Value.ToString();
            if (DateTime.TryParse(text, out DateTime DateTimeValue))
            {
                FromDate = DateTimeValue.ToString("MM-dd-yyyy");
                return DateSelected.InvokeAsync(DateTimeValue);
            }
            return DateSelected.InvokeAsync(null);
        }

        private Task OnToDateSelected(ChangeEventArgs args)
        {
            var text = args.Value.ToString();
            if (DateTime.TryParse(text, out DateTime DateTimeValue))
            {
                ToDate = DateTimeValue.ToString("MM-dd-yyyy");
                return DateSelected.InvokeAsync(DateTimeValue);
            }
            return DateSelected.InvokeAsync(null);
        }

        private Task OnFromDosSelected(ChangeEventArgs args)
        {
            var text = args.Value.ToString();
            if (DateTime.TryParse(text, out DateTime DateTimeValue))
            {
                FromDos = DateTimeValue.ToString("MM-dd-yyyy");
                return DateSelected.InvokeAsync(DateTimeValue);
            }
            return DateSelected.InvokeAsync(null);
        }

        private Task OnTosSelected(ChangeEventArgs args)
        {
            var text = args.Value.ToString();
            if (DateTime.TryParse(text, out DateTime DateTimeValue))
            {
                ToDos = DateTimeValue.ToString("MM-dd-yyyy");
                return DateSelected.InvokeAsync(DateTimeValue);
            }
            return DateSelected.InvokeAsync(null);
        }

        public async Task GetWorkqueue(int PGroupID)
        {
            try
            {
                DisplayLoading(true, "Loading");
                isWorkQueueHide = false;
                var AuthState = (AuthStateProvider)AuthProvider;
                dsworkqueue = await ApiHelper.GetUsingMsgPackAsync<GetWorkQueueResults[]>($"InventoryDashboard/GetWorkQueue/EmployeeID/" + EmpID + "/ProjectGroupID/" + PGroupID, Http, AuthState.Token);
                selectedWorkQueue = dsworkqueue.Select(x => x.WorkQueueValue.ToString()).ToList();
                WorkQueue = string.Join(',', selectedWorkQueue).ToString();
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                // Logger.LogError(ex, ex.Message);
                // ToasterService.AddDanger("Inventory Dashboard", "Error occured while loading Work Queue");
                Toaster.Add("Error occured while loading Work Queue", MatToastType.Danger, "Inventory Dashboard");
                DisplayLoading(false);
            }
            finally
            {
                StateHasChanged();
            }
        }

        public async Task GetInventoryMasters(int PGroupID)
        {
            try
            {
                DisplayLoading(true, "Loading");
                isQueueNameHide = false;
                var AuthState = (AuthStateProvider)AuthProvider;
                dsInventoryMaster = await ApiHelper.GetUsingMsgPackAsync<GetInventoryMastersResults[]>($"InventoryDashboard/GetInventoryMasters/EmployeeID/" + EmpID + "/ProjectGroupID/" + PGroupID, Http, AuthState.Token);
                dsQueueName = dsInventoryMaster[0].QueueName.ToArray();
                selectedQueueName = dsQueueName.Select(x => x.MasterValue.ToString()).ToList();
                QueueName = string.Join(',', selectedQueueName).ToString();
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                // ToasterService.AddDanger("Inventory Dashboard", "Error occured while loading QueueName");
                Toaster.Add("Error occured while loading Work Queue", MatToastType.Danger, "Inventory Dashboard");
                DisplayLoading(false);
            }
            finally
            {
                StateHasChanged();
            }
        }

        private async void GetInventory()
        {
            try
            {
                if (string.IsNullOrEmpty(FromDate))
                {
                    Toaster.Add("Kindly choose From Date", MatToastType.Warning, "Inventory Dashboard");
                    return;
                }
                else if (string.IsNullOrEmpty(ToDate))
                {
                    Toaster.Add("Kindly choose To Date", MatToastType.Warning, "Inventory Dashboard");
                    return;
                }
                DateTime dt1 = DateTime.ParseExact(FromDate, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                DateTime dt2 = DateTime.ParseExact(ToDate, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                if (dt1 > dt2)
                {
                    Toaster.Add("To Date should be greater than From date", MatToastType.Warning, "Inventory Dashboard");
                }
                else if (PGroupID == 6582 && !selectedWorkQueue.Any())
                {
                    Toaster.Add("Kindly select the work queue number", MatToastType.Warning, "Inventory Dashboard");
                }
                else if ((PGroupID == 6632 || PGroupID == 6633) && !selectedQueueName.Any())
                {
                    Toaster.Add("Kindly select the Queue Name", MatToastType.Warning, "Inventory Dashboard");
                }
                else
                {
                    await GetInventoryResult();
                }
            }
            catch (Exception ex)
            {
                Toaster.Add("Error in get inventory" + ex.Message, MatToastType.Danger, "Inventory Dashboard");
            }

        }
        private async Task GetInventoryResult()
        {
            try
            {
                DisplayLoading(true, "Loading");
                if (FromDos == null || ToDos == null)
                {
                    FromDos = "EMPTY"; ToDos = "EMPTY";
                }
                if (PGroupID != 6582)
                {
                    WorkQueue = "EMPTY";
                }
                else
                {
                    WorkQueue = string.Join(',', selectedWorkQueue).ToString();
                }

                if (PGroupID != 6632 && PGroupID != 6633)
                {
                    QueueName = "EMPTY";
                }
                else
                {
                    QueueName = string.Join(',', selectedQueueName).ToString();
                }
                
                var AuthState = (AuthStateProvider)AuthProvider;
                var InventoryInput = new GetInventoryInput()
                {
                    EmployeeID = EmpID,
                    ProjectGroupId = PGroupID,
                    FromDate = FromDate,
                    ToDate = ToDate,
                    FromDOS = FromDos,
                    ToDOS = ToDos,
                    WorkQueue = WorkQueue,
                    QueueName = QueueName,
                    SchemaName = schemaName
                };


                // string Inventoryurl = "/InventoryDashboard/GetInventoryDashBoard/EmployeeID/" + EmpID + "/ProjectGroupId/"
                // + PGroupID + "/FromDate/" + FromDate + "/ToDate/" + ToDate + "/FromDOS/" + FromDos + "/ToDOS/" + ToDos +
                // "/WorkQueue/" + WorkQueue + "/QueueName/" + QueueName + "/SchemaName/" + this.schemaName;
                _InventorySummary = await
                ApiHelper.PostUsingMsgPackAsync<GetInventoryResults[]>("/InventoryDashboard/GetInventoryDashBoard",InventoryInput, Http, AuthState.Token);
                if (_InventorySummary[0].Inventory.Count() > 0)
                {
                    isHide = false;
                    _InventoryData = _InventorySummary[0].Inventory.ToArray();
                }
                else
                {
                    isHide = true;
                    Toaster.Add("No Accounts Found", MatToastType.Danger);
                }
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                isHide = true;
                // ToasterService.AddWarning("Inventory Dashboard", "Error occured while loading Inventory");
                Toaster.Add("Error occured while loading Inventory", MatToastType.Danger, "Inventory Dashboard");
                DisplayLoading(false);
            }
            finally
            {
                StateHasChanged();
            }
        }
    }

}