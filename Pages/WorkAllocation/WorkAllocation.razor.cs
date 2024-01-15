using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ChargesWFM.UI.Models;
using ChargesWFM.UI.Services;
using ChargesWFM.UI.Components;
using Microsoft.JSInterop;
using MatBlazor;

namespace ChargesWFM.UI.Pages.WorkAllocation
{
    public partial class WorkAllocation : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }
        [Inject]
        protected AuthenticationStateProvider AuthProvider { get; set; }
        [Inject]
        ILocalStorageService? LocalStorageService { get; set; }
        [Inject]
        IMatToaster Toaster { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [Inject]
        IHotKeyService HotKeyService { get; set; }
        public int ProjectGroupID { get; set; }
        public string? ProjectGroupName { get; set; }
        public string? SchemaName = string.Empty;
        private string? progressText { get; set; }
        private bool displayLoading;
        public bool displayProgress;
        private AuthenticatedUser? user;
        private WorkallocationAccountSummary[]? _uploadsummary = Array.Empty<WorkallocationAccountSummary>();
        private DialogNew modal;
        public bool IsEditable;
        public bool IsBillableGroupDisable;
        public bool IsSubClientDisable;
        public bool IsSoftwareDisable;
        public bool IsTask;
        public bool IsSubTask;
        private bool isHide = true;
        public string EditSubClient { get; set; }
        private string EditSoftware { get; set; }
        private string EditBillableGroup { get; set; }
        private string? selectedClientLogin { get; set; }
        public string? screenName { get; set; }
        private List<EmployeeAccountDetails> editableAccounts = new List<EmployeeAccountDetails>();
        private List<EditableFields> editableFields = new List<EditableFields>();
        public bool ShowPage { get; set; }
        public bool HasSkip { get; set; }
        private IEnumerable<CodingFieldTypes> FieldTypes = Enumerable.Empty<CodingFieldTypes>();
        protected override async Task OnInitializedAsync()
        {
            user = await LocalStorageService.GetAsync<AuthenticatedUser>();
            var projectGroup = await LocalStorageService.GetAsync<NexgenProjectGroups>();
            this.screenName = "WorkAllocation";
            this.isHide = false;
            if (projectGroup != null)
            {
                ProjectGroupID = projectGroup.ProjectGroupID;
                SchemaName = projectGroup.SchemaName;
            }
        }
        protected async Task OnProjectGroupChanged(NexgenProjectGroups pg)
        {
            this.ProjectGroupID = pg.ProjectGroupID;
            this.ProjectGroupName = pg.ProjectGroupName;
            this.SchemaName = pg.SchemaName;
            this.IsEditable = false;
            this.isHide = false;
            displayProgress = true;
            editableAccounts = new List<EmployeeAccountDetails>();
            this.StateHasChanged();
            displayProgress = false;
        }
        public async Task uploadsummary()
        {
            try
            {
                DisplayLoading(true, "Loading");
                var EmployeeID = user.EmployeeId;
                var AuthState = (AuthStateProvider)AuthProvider;
                string apiURL = $"WorkAllocation/GetWorkAllocationsSummary/ProjectGroupID/" + ProjectGroupID + "/EmployeeID/" + EmployeeID + "/SchemaName/" + SchemaName;
                _uploadsummary = await ApiHelper.GetUsingMsgPackAsync<WorkallocationAccountSummary[]>(apiURL, Http, AuthState.Token);
                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error in upload summary", MatToastType.Danger, "Work Allocation");
                DisplayLoading(false);
            }
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                HotKeyService.OpenFetchAction += OpenFetchAction;
            }
        }

        public void Dispose()
        {
            HotKeyService.OpenFetchAction -= OpenFetchAction;
        }

        private async void OpenFetchAction(object sender, EventArgs e)
        {
            EditAccountDetails();
        }

        private async void OpenDialog()
        {
            await uploadsummary();
            modal.Open();
        }
        private async Task btnDownload()
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                var EmployeeID = user.EmployeeId;
                string apiURL = $"WorkAllocation/DownloadWorkAllocationExcel/ProjectGroupID/" + ProjectGroupID + "/EmployeeID/" + EmployeeID + "/SchemaName/" + SchemaName;
                var columnNames = await ApiHelper.GetUsingMsgPackAsync<WorkAllocationExcelTemplate>(apiURL, Http, AuthState.Token);
                var downloadExcelHeaders = columnNames.ColumnNames.Select(x => x.FieldName).ToList();
                var downloadExcelHeadersDisplay = columnNames.ColumnNames.Select(x => x.DisplayName).ToList();
                var bytes = await ExcelPackage.GenerateTemplateAsync1(downloadExcelHeaders.ToArray(), downloadExcelHeadersDisplay.ToArray(), columnNames.UploadAccountDetails);
                await BlazorDownloadFileService.DownloadFile("AccountDetails.xlsx", bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error in download", MatToastType.Danger, "Work Allocation");
                DisplayLoading(false);
            }

        }
        public async void EditAccountDetails()
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                var EmployeeID = user.EmployeeId;
                var projectGroup = await LocalStorageService!.GetAsync<NexgenProjectGroups>();
                await GetCodingFieldTypes();
                if(!FieldTypes.Any(exp => exp.FieldType == "WorkAllocation Page"))
                {
                    Toaster.Add("Access Denied For Allocation Page", MatToastType.Danger, "Work Allocation");
                    DisplayLoading(false);
                    return;
                }

                string apiURL = $"WorkAllocation/GetWorkAllocations/ProjectGroupID/{projectGroup.ProjectGroupID}/EmployeeID/{EmployeeID}/SchemaName/{this.SchemaName}";
                var AccountDetailData = await ApiHelper.GetUsingMsgPackAsync<WorkAllocationAccounts[]>(apiURL, Http, AuthState.Token);
                if(AccountDetailData != null && AccountDetailData.Count() > 0)
                {
                    if (AccountDetailData[0].AllocatedAccounts.Count() == 0)
                    {
                        Toaster.Add("You have Already Fetched the other Account.!", MatToastType.Danger, "Search and Code");
                    }

                    if (AccountDetailData[0].AllocatedAccounts.Count() > 0)
                    {
                        var Accounts = AccountDetailData[0].AllocatedAccounts.ToList();
                        editableAccounts.Add(Accounts.First());
                        EditSubClient = Accounts.First().SubClient;
                        EditBillableGroup = Accounts.First().BillableGroup;
                        EditSoftware = Accounts.First().Software;
                        this.screenName = "WorkAllocation";
                        this.HasSkip = AccountDetailData[0].HasSkip;
                        this.IsEditable = true;
                        this.isHide = true;
                    }
                    if (AccountDetailData[0].EditableFields.Count() > 0)
                    {
                        this.IsBillableGroupDisable = AccountDetailData[0].EditableFields.Any(exp => exp.CodingField == "BillableGroupID" && exp.IsEditable == true);
                        this.IsSubClientDisable = AccountDetailData[0].EditableFields.Any(exp => exp.CodingField == "SubClientID" && exp.IsEditable == true);
                        this.IsSoftwareDisable = AccountDetailData[0].EditableFields.Any(exp => exp.CodingField == "SubClientID" && exp.IsEditable == true);
                        this.IsTask = AccountDetailData[0].EditableFields.Any(exp => exp.CodingField == "Task" && exp.IsEditable == true);
                        this.IsSubTask = AccountDetailData[0].EditableFields.Any(exp => exp.CodingField == "SubTask" && exp.IsEditable == true);
                        this.editableFields = AccountDetailData[0].EditableFields.ToList();
                    }
                }
                else
                {
                    Toaster.Add("No Accounts Available", MatToastType.Danger, "Work Allocation");
                }
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                this.IsEditable = false;
                this.isHide = false;
                Logger.LogError(ex, ex.Message);
                Toaster.Add("No Accounts Available", MatToastType.Danger, "Work Allocation");
                DisplayLoading(false);
            }
            finally
            {
                StateHasChanged();
            }
        }
        private async Task GetCodingFieldTypes()
        {
            try
            {
                DisplayLoading(true, "Loading");
                var projectGroup = await LocalStorageService!.GetAsync<NexgenProjectGroups>();
                var AuthState = (AuthStateProvider)AuthProvider!;
                FieldTypes = await
                ApiHelper.GetUsingMsgPackAsync<CodingFieldTypes[]>($"/EditTransaction/GetCodingFieldTypes/ProjectGroupID/{projectGroup.ProjectGroupID}",
                Http,
                AuthState!.Token);
                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);               
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                DisplayLoading(false);
            }
            finally
            {
                StateHasChanged();
            }
        }
        private void DisplayLoading(bool display, string? progressMessage = null)
        {
            displayLoading = display;
            progressText = progressMessage ?? string.Empty;
            StateHasChanged();
        }
    }
}