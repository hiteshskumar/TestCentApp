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
using OfficeOpenXml.Export.ToDataTable;
// inject IBlazorDownloadFileService BlazorDownloadFileService

namespace ChargesWFM.UI.Pages.ManualAssign
{
    public partial class ManualAssign
    {
        [Inject]
        HttpClient Http { get; set; }
        [Inject]
        AuthenticationStateProvider AuthProvider { get; set; }
        [Inject]
        ILocalStorageService LocalStorageService { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [Inject]
        IToasterService? ToasterService { get; set; }
        [Inject]
        IMatToaster Toaster { get; set; }
        [Inject]
        IBlazorDownloadFileService BlazorDownloadFileService { get; set; }
        [Inject]
        ILogger<ManualAssign> Logger { get; set; }
        private MAssign[]? _AutoCompleteUploadTemplate;
        private ProcessEmployee[]? _AutoCompleteProcessEmployee;
        private IEnumerable<MAssign> _UploadTemplateform = Enumerable.Empty<MAssign>();
        private IEnumerable<ProcessEmployee> _ProcessEmployeesform = Enumerable.Empty<ProcessEmployee>();
        public MAssign _selectedUploadTemplate = new MAssign();
        public ProcessEmployee _selectedProcessEmployee = new ProcessEmployee();
        private Tuple<string, GridSortOrder> sortColumn;
        public int EmpID = 0;
        public IEnumerable<GetSearchAndCodeResult> _Accountinfo { get; set; } = Enumerable.Empty<GetSearchAndCodeResult>();
        private List<Tuple<PropertyInfo, string>> AccountProperty = new List<Tuple<PropertyInfo, string>>();
        public EventCallback<IEnumerable<GetSearchAndCodeResult>> ItemsChanged { get; set; }

        public EventCallback<IEnumerable<GetValidationSGBOTGrid>> BOTItemsChanged { get; set; }
        private int pageSize = 50;
        private bool isHide = true;
        private bool isSGHide = true;
        private bool isShow = true;
        private bool isSGGridHide = true;
        private bool isUnAssignedBatchSelect = false;
        public EventCallback<IEnumerable<GetSearchAndCodeResult>> BulkCheckedChanged { get; set; }
        public EventCallback<IEnumerable<GetValidationSGBOTGrid>> BOTBulkCheckedChanged { get; set; }
        private string? progressText { get; set; }
        private FieldAccountMapping[]? dsSelectedUniqueField;
        private DownloadExcelManualAssign[]? dsExcelAccountDetails;
        List<string> downloadExcelHeaders = new List<string>();
        List<string> downloadExcelHeadersDisplay = new List<string>();
        private bool displayLoading;
        private AuthenticatedUser? user;
        private int uploadTemplateID = 0;
        private string uploadTemplateName = string.Empty;
        private int PGroupID = 0;
        // private int ProjectGroupID { get; set; }
        private string? ProjectGroupName { get; set; }
        public string schemaName = string.Empty;
        private List<SearchCriteria> searchCriterias = new List<SearchCriteria>();
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        private IEnumerable<GetValidationSGBOTGrid> _ValidationSGGrid = Enumerable.Empty<GetValidationSGBOTGrid>();
        public EventCallback<DateTime?> DateSelected { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var user = await LocalStorageService.GetAsync<AuthenticatedUser>();
            EmpID = user.EmployeeId;

            var projectGroup = await LocalStorageService.GetAsync<NexgenProjectGroups>();
            PGroupID = projectGroup.ProjectGroupID;
            schemaName = projectGroup.SchemaName;
            AccountProperty.Clear();
            // if (projectGroup != null)
            // {
            //     await GetUploadTemplates(projectGroup.ProjectGroupID);
            //     await GetProcessEmployees(projectGroup.ProjectGroupID);
            // }
            if (PGroupID == 7849)
            {
                isSGHide = false;
                isShow = true;
                isHide = true;
                isSGGridHide = true;
            }
            else
            {
                isSGHide = true;
                isShow = false;
                isHide = true;
                isSGGridHide = true;
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
            this.ProjectGroupName = pg.ProjectGroupName;
            this.schemaName = pg.SchemaName;
            if (PGroupID == 7849)
            {
                isSGHide = false;
                isShow = true;
            }
            else
            {
                isSGHide = true;
                isShow = false;
            }

            AccountProperty.Clear();
            await GetUploadTemplates(pg.ProjectGroupID);
            await GetProcessEmployees(pg.ProjectGroupID);
            this.StateHasChanged();
        }

        private async Task GetUploadTemplates(int projectGroup)
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                _AutoCompleteUploadTemplate = await
                ApiHelper.GetUsingMsgPackAsync<MAssign[]>("/ManualAssign/GetUploadTemplates/EmployeeID/" + EmpID +
                "/ProjectGroupID/" + projectGroup + "", Http,
                AuthState.Token);
                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);

                if (_AutoCompleteUploadTemplate.Count() > 0)
                {
                    this._UploadTemplateform = _AutoCompleteUploadTemplate.Select(x => new MAssign
                    {
                        AllocationType = x.AllocationType,
                        AllocationMethod = x.AllocationMethod,
                        AllocateToPreviouslyWorkedAgents = x.AllocateToPreviouslyWorkedAgents,
                        AllocateToIdleAgents = x.AllocateToIdleAgents,
                        AllocateMultipleAgents = x.AllocateMultipleAgents,
                        AppendUpload = x.AppendUpload,
                        Skip = x.Skip,
                        IsWorkableFilter = x.IsWorkableFilter,
                        UploadTemplateID = x.UploadTemplateID,
                        UploadTemplateName = x.UploadTemplateName,
                        AllocateToSkipAccounts = x.AllocateToSkipAccounts,
                        ProcessID = x.ProcessID,
                        SearchandEdit = x.SearchandEdit
                    }).ToList();
                    var selUploadTemplate = _UploadTemplateform.ToList();
                    _selectedUploadTemplate = selUploadTemplate[0];
                    uploadTemplateID = _selectedUploadTemplate.UploadTemplateID;
                    await OnUploadTemplateSelected(_selectedUploadTemplate);
                }
                else
                {
                    isHide = true;
                    isSGGridHide = true;
                    _UploadTemplateform = Enumerable.Empty<MAssign>();
                    _selectedUploadTemplate = null;
                    // Toaster.Add("Upload Template Type not Mapped", MatToastType.Danger, "Manual Assign");
                    Toaster.Add("Upload Template Type not Mapped", MatToastType.Danger);
                }
                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
                DisplayLoading(false);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                // ToasterService.AddDanger("ManualAssign", ex.Message);
                Toaster.Add(ex.Message.ToString(), MatToastType.Danger, "Manual Assign");
                DisplayLoading(false);
            }
        }

        private async Task OnUploadTemplateSelected(MAssign selectedUploadTemplate)
        {
            AccountProperty.Clear();
            _selectedUploadTemplate = selectedUploadTemplate;
            uploadTemplateID = _selectedUploadTemplate.UploadTemplateID;
            await FetchSelectedUniqueFields(selectedUploadTemplate.UploadTemplateID, PGroupID);
        }

        private async Task GetProcessEmployees(int projectGroupid)
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                _AutoCompleteProcessEmployee = await
                ApiHelper.GetUsingMsgPackAsync<ProcessEmployee[]>("/ManualAssign/GetProjectGroupEmployees/EmployeeID/" + EmpID + "/ProjectGroupID/" + projectGroupid + "", Http,
                AuthState.Token);
                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false); if (_AutoCompleteProcessEmployee.Count() > 0)
                {
                    this._ProcessEmployeesform = _AutoCompleteProcessEmployee.Select(x => new ProcessEmployee
                    {
                        SigninName = x.SigninName,
                        EmployeeCode = x.EmployeeCode,
                        EmployeeID = x.EmployeeID
                    }).ToList();
                }

                ProcessEmployee _selectedProcessEmployee = new ProcessEmployee();
                var selProcessEmployee = _ProcessEmployeesform.ToList();
                _selectedProcessEmployee.EmployeeID = selProcessEmployee[0].EmployeeID;
                _selectedProcessEmployee.EmployeeCode = selProcessEmployee[0].EmployeeCode;
                _selectedProcessEmployee.SigninName = selProcessEmployee[0].SigninName;
                await OnProcessEmployeeSelected(_selectedProcessEmployee);
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                // ToasterService.AddDanger("ManualAssign", ex.Message);
                Toaster.Add(ex.Message.ToString(), MatToastType.Danger, "Manual Assign");
                DisplayLoading(false);
            }
        }

        private Task OnProcessEmployeeSelected(ProcessEmployee selectedProcessEmployee)
        {
            _selectedProcessEmployee = selectedProcessEmployee;
            return Task.CompletedTask;
        }

        private async Task btnDownload()
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;

                dsExcelAccountDetails = await
                    ApiHelper.GetUsingMsgPackAsync<DownloadExcelManualAssign[]>
                    ("/ManualAssign/DownloadExcelManualAssign/UploadTemplateID/" + uploadTemplateID +
                    "/Filter/ManualAssign/ProjectGroupID/" + PGroupID + "/SchemaName/" + schemaName + "",
                    Http, AuthState.Token);

                downloadExcelHeaders.Clear(); downloadExcelHeadersDisplay.Clear();
                var type = typeof(GetSearchAndCodeResult);
                if (!downloadExcelHeaders.Contains("SubClient"))
                    downloadExcelHeaders.Add("SubClient");
                if (!downloadExcelHeaders.Contains("BillableGroup"))
                    downloadExcelHeaders.Add("BillableGroup");
                if (!downloadExcelHeaders.Contains("Software"))
                    downloadExcelHeaders.Add("Software");
                if (!downloadExcelHeadersDisplay.Contains("SubClient"))
                    downloadExcelHeadersDisplay.Add("SubClient");
                if (!downloadExcelHeadersDisplay.Contains("BillableGroup"))
                    downloadExcelHeadersDisplay.Add("BillableGroup");
                if (!downloadExcelHeadersDisplay.Contains("Software"))
                    downloadExcelHeadersDisplay.Add("Software");
                if (dsExcelAccountDetails[0].Accountdetails!.Count() > 0)
                {
                    foreach (var item in dsExcelAccountDetails[0].FieldResult)
                    {
                        if (!downloadExcelHeaders.Contains(item.Field))
                            downloadExcelHeaders.Add(item.Field);
                        if (!downloadExcelHeadersDisplay.Contains(item.DisplayName))
                            downloadExcelHeadersDisplay.Add(item.DisplayName);
                    };

                }
                if (!downloadExcelHeaders.Contains("Employee"))
                    downloadExcelHeaders.Add("Employee");
                if (!downloadExcelHeadersDisplay.Contains("Employee"))
                    downloadExcelHeadersDisplay.Add("Employee");


                // var header = PlacementFields.Where(x => x.IsLLUnique == true).Select(x => Tuple.Create(x.FieldDetails.Field, x.DisplayName)).ToList();
                // var header = AccountProperty.Select(x => Tuple.Create(type.GetProperty(x.Item2), x.Item2)).ToArray();
                // header.Add(Tuple.Create("ARComments","ARComments"));
                var bytes = await ExcelPackage.GenerateTemplateAsync1(downloadExcelHeaders.ToArray(), downloadExcelHeadersDisplay.ToArray(), dsExcelAccountDetails[0].Accountdetails);
                await BlazorDownloadFileService.DownloadFile("AccountDetails.xlsx", bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                // ToasterService.AddDanger("ManualAssign", ex.Message);
                Toaster.Add(ex.Message.ToString(), MatToastType.Danger, "Manual Assign");
                DisplayLoading(false);
            }
        }
        private Task OnCheckedChanged(Tuple<GetSearchAndCodeResult, bool> args)
        {
            args.Item1.IsChecked = args.Item2;
            return Task.CompletedTask;
        }

        private Task OnBOTCheckedChanged(Tuple<GetValidationSGBOTGrid, bool> args)
        {
            args.Item1.IsBOTChecked = args.Item2;
            return Task.CompletedTask;
        }

        private Task OnItemsChanged(IEnumerable<GetSearchAndCodeResult> items)
        {
            return ItemsChanged.InvokeAsync(items);
        }

        private Task OnBOTItemsChanged(IEnumerable<GetValidationSGBOTGrid> items)
        {
            return BOTItemsChanged.InvokeAsync(items);
        }

        private Task OnClearSelection(bool value)
        {
            foreach (var account in _Accountinfo)
            {
                account.IsChecked = value;
            }
            return Task.CompletedTask;
        }

        private Task OnBOTClearSelection(bool value)
        {
            foreach (var Bot in _ValidationSGGrid)
            {
                Bot.IsBOTChecked = value;
            }
            return Task.CompletedTask;
        }

        private Task OnBulkCheckedChanged(Tuple<IEnumerable<GetSearchAndCodeResult>, bool> args)
        {
            foreach (var item in args.Item1)
            {
                item.IsChecked = args.Item2;
            }
            return BulkCheckedChanged.InvokeAsync(args.Item1);
        }

        private Task OnBOTBulkCheckedChanged(Tuple<IEnumerable<GetValidationSGBOTGrid>, bool> args)
        {
            foreach (var item in args.Item1)
            {
                item.IsBOTChecked = args.Item2;
            }
            return BOTBulkCheckedChanged.InvokeAsync(args.Item1);
        }
        private async void updateAccounts()
        {
            if (PGroupID != 7849)
            {
                try
                {
                    DisplayLoading(true, "Loading");
                    GetAssignUploadDetails updateAcc = new GetAssignUploadDetails();
                    var selectAccountID = GetSelectedAccounts();

                    if (!(_selectedProcessEmployee != null))
                    {
                        Toaster.Add("Please choose Employee to Assign", MatToastType.Warning, "ManualAssign");
                    }
                    else if (isUnAssignedAccountSelect)
                    {
                        Toaster.Add("Please select assigned accounts to unassign", MatToastType.Warning, "ManualAssign");
                    }
                    else if (!selectAccountID.Any())
                    {
                        Toaster.Add("Please choose accounts to update", MatToastType.Warning, "ManualAssign");
                    }
                    else
                    {
                        var AuthState = (AuthStateProvider)AuthProvider;
                        var listAccIDs = string.Empty;

                        updateAcc.UploadTemplateID = uploadTemplateID;
                        updateAcc.Accounts = selectAccountID.ToArray();
                        updateAcc.ProjectGroupEmployee = _selectedProcessEmployee.EmployeeID;
                        updateAcc.EmployeeID = EmpID;
                        updateAcc.ProjectGroupID = PGroupID;
                        updateAcc.SchemaName = schemaName;

                        var status = await ApiHelper.PostUsingMsgPackAsync<StatusMessageResponse>
                        ("/manualassign/SaveNewAccountDetails", updateAcc, Http, AuthState.Token);
                        if (status != null)
                        {
                            if (status.Status.Contains("Account Already Fetched"))
                            {
                                Toaster.Add("Account Already Fetched", MatToastType.Danger, "ManualAssign");
                            }
                            else
                            {
                                if (updateAcc.ProjectGroupEmployee == 0)
                                {
                                    if (status.Status.Contains("Success count = "))
                                    {
                                        string check = "Success count = " + selectAccountID.Count();
                                        if (status.Status == check)
                                        {
                                            Toaster.Add("UnAssigned successfully", MatToastType.Success, "ManualAssign");
                                        }
                                        else
                                        {
                                            Toaster.Add("Un Fetched Accounts are UnAssigned successfully", MatToastType.Success, "ManualAssign");
                                        }
                                    }
                                }
                                else
                                {
                                    if (status.Status.Contains("Success count = "))
                                    {
                                        string check = "Success count = " + selectAccountID.Count();
                                        if (status.Status == check)
                                        {
                                            Toaster.Add("Assigned successfully", MatToastType.Success, "ManualAssign");
                                        }
                                        else
                                        {
                                            Toaster.Add("Un Fetched Accounts are Assigned successfully", MatToastType.Success, "ManualAssign");
                                        }
                                    }
                                }
                            }

                            _selectedProcessEmployee = new ProcessEmployee();
                            _selectedProcessEmployee.SigninName = string.Empty;
                            await FetchSelectedUniqueFields(updateAcc.UploadTemplateID, PGroupID);
                        }
                        else
                        {
                            Toaster.Add("Update failed", MatToastType.Danger, "ManualAssign");
                        }
                    }
                    DisplayLoading(false);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, ex.Message);
                    Toaster.Add(ex.Message.ToString(), MatToastType.Danger, "Manual Assign");
                    DisplayLoading(false);
                }
                finally
                {
                    StateHasChanged();
                }
            }
            else
            {
                try
                {
                    DisplayLoading(true, "Loading");
                    GetAssignUploadDetails updateAcc = new GetAssignUploadDetails();

                    var selectAccountID = GetSelectedAccounts();
                    var selectBatch = GetSelectedBatch();

                    if (!(_selectedProcessEmployee != null))
                    {
                        Toaster.Add("Please choose Employee to Assign", MatToastType.Warning, "ManualAssign");
                    }
                    else if (isUnAssignedBatchSelect)
                    {
                        Toaster.Add("Please select assigned Batch to unassign", MatToastType.Warning, "ManualAssign");
                    }
                    else if (!selectBatch.Any())
                    {
                        Toaster.Add("Please choose Batch to update", MatToastType.Warning, "ManualAssign");
                    }
                    else
                    {
                        var AuthState = (AuthStateProvider)AuthProvider;
                        var listAccIDs = string.Empty;
                        Console.WriteLine(_selectedProcessEmployee.EmployeeID);
                        updateAcc.UploadTemplateID = uploadTemplateID;
                        updateAcc.Accounts = selectAccountID.ToArray();
                        updateAcc.ProjectGroupEmployee = _selectedProcessEmployee.EmployeeID;
                        updateAcc.EmployeeID = EmpID;
                        updateAcc.ProjectGroupID = PGroupID;
                        updateAcc.SchemaName = schemaName;
                        updateAcc.BatchDetails = selectBatch.ToArray();


                        var status = await ApiHelper.PostUsingMsgPackAsync<StatusMessageResponse>
                        ("/manualassign/SaveNewAccountDetails", updateAcc, Http, AuthState.Token);

                        if (status != null)
                        {
                            if (status.Status.Contains("Batch Already Fetched"))
                            {
                                Toaster.Add("Batch Already Fetched", MatToastType.Danger, "ManualAssign");
                            }
                            else
                            {
                                if (updateAcc.ProjectGroupEmployee == 0)
                                {
                                    if (status.Status.Contains("Success count = "))
                                    {
                                        string check = "Success count = " + selectBatch.Count();
                                        if (status.Status == check)
                                        {
                                            Toaster.Add("UnAssigned successfully", MatToastType.Success, "ManualAssign");
                                        }
                                        else
                                        {
                                            Toaster.Add("Un Fetched Batch Accounts are UnAssigned successfully", MatToastType.Success, "ManualAssign");
                                        }
                                    }
                                }
                                else
                                {
                                    if (status.Status.Contains("Success count = "))
                                    {
                                        string check = "Success count = " + selectBatch.Count();
                                        if (status.Status == check)
                                        {
                                            Toaster.Add("Assigned successfully", MatToastType.Success, "ManualAssign");
                                        }
                                        else
                                        {
                                            Toaster.Add("Un Fetched Batch are Assigned successfully", MatToastType.Success, "ManualAssign");
                                        }
                                    }
                                }
                            }

                            _selectedProcessEmployee = new ProcessEmployee();
                            _selectedProcessEmployee.SigninName = string.Empty;
                            await FetchSelectedUniqueFields(updateAcc.UploadTemplateID, PGroupID);
                        }
                        else
                        {
                            Toaster.Add("Update failed", MatToastType.Danger, "ManualAssign");
                        }
                    }
                    DisplayLoading(false);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, ex.Message);
                    Toaster.Add(ex.Message.ToString(), MatToastType.Danger, "Manual Assign");
                    DisplayLoading(false);
                }
                finally
                {
                    StateHasChanged();
                }
            }

        }
        bool isUnAssignedAccountSelect = false;
        private List<string> GetSelectedAccounts()
        {
            isUnAssignedAccountSelect = false;
            var AccountIDs = new List<string>();
            foreach (var placementAcc in _Accountinfo)
            {
                if (placementAcc.IsChecked)
                {
                    if (string.IsNullOrEmpty(placementAcc.Assigned) && _selectedProcessEmployee.EmployeeID == 0)
                    {
                        isUnAssignedAccountSelect = true;
                    }
                    AccountIDs.Add(placementAcc.UploadID.ToString());
                }
            }
            return AccountIDs;
        }
        private List<GetValidationSGBOTGrid> GetSelectedBatch()
        {
            isUnAssignedBatchSelect = false;
            List<GetValidationSGBOTGrid> BatchDetails = new();
            foreach (var BOT in _ValidationSGGrid)
            {
                if (BOT.IsBOTChecked)
                {
                    if (string.IsNullOrEmpty(BOT.Assigned) && _selectedProcessEmployee.EmployeeID == 0)
                    {
                        isUnAssignedBatchSelect = true;
                    }
                    BatchDetails.Add(new GetValidationSGBOTGrid()
                    {
                        BatchName = BOT.BatchName,
                        TotalDocuments = BOT.TotalDocuments,
                        TotalPages = BOT.TotalPages,
                        FetchStatus = BOT.FetchStatus,
                        AssignStatus = BOT.AssignStatus
                    });
                    Console.WriteLine(BOT.AssignStatus);
                }
            }
            return BatchDetails;
        }
        private async Task FetchSelectedUniqueFields(int UploadTemplateID, int projectGroup)
        {
            try
            {
                if (projectGroup != 7849)
                {
                    DisplayLoading(true, "Loading");
                    isSGGridHide = true;
                    isHide = true;
                    var data = new GetAccount()
                    {
                        UploadTemplateID = uploadTemplateID,
                        Filter = "EMPTY",
                        ProjectGroupID = PGroupID,
                        SchemaName = schemaName
                    };
                    var AuthState = (AuthStateProvider)AuthProvider;
                    dsSelectedUniqueField = await ApiHelper.PostUsingMsgPackAsync<FieldAccountMapping[]>("/ManualAssign/GetFieldAccountMapping", data, Http, AuthState.Token);

                    AccountProperty.Clear();
                    AccountProperty = new List<Tuple<PropertyInfo, string>>();
                    var type = typeof(GetSearchAndCodeResult);

                    if (dsSelectedUniqueField != null && dsSelectedUniqueField.Count() > 0 && dsSelectedUniqueField[0].Accountdetails.Count() > 0)
                    {
                        foreach (var item in dsSelectedUniqueField[0].FieldResult)
                        {
                            AccountProperty.Add(Tuple.Create(type.GetProperty(item.Field), item.DisplayName));
                        };
                        AccountProperty.Add(Tuple.Create(type.GetProperty("Assigned"), "Assigned"));
                        foreach (var item in dsSelectedUniqueField[0].Accountdetails)
                        {
                            _Accountinfo = dsSelectedUniqueField[0].Accountdetails;
                        };
                        isHide = false;
                    }
                    else
                    {
                        isHide = true;
                        Toaster.Add("No Accounts Available", MatToastType.Danger, "Manual Assign");
                    }
                    DisplayLoading(false);
                }
                else
                {
                    DisplayLoading(true, "Loading");
                    isSGGridHide = true;
                    var AuthState = (AuthStateProvider)AuthProvider;
                    FromDate ??= DateTime.Now.ToString("M-d-yyyy");
                    ToDate ??= DateTime.Now.ToString("M-d-yyyy");
                    Console.WriteLine("/ManualAssign/GetValidationSGBOTGrid/FromDate/" + FromDate + "/ToDate/" + ToDate + "/Schema/" + schemaName);
                    
                    _ValidationSGGrid = await ApiHelper.GetUsingMsgPackAsync<GetValidationSGBOTGrid[]>
                    ("/ManualAssign/GetValidationSGBOTGrid/FromDate/" + FromDate + "/ToDate/" + ToDate + "/Schema/" + schemaName, Http, AuthState.Token);
                    //Console.WriteLine(_ValidationSGGrid.Count());
                    if (_ValidationSGGrid.Count() == 0)
                    {
                        isSGGridHide = true;
                        Toaster.Add("No Accounts for the select project group", MatToastType.Danger, "Manual Assign");
                    }
                    else
                    {
                        //Console.WriteLine("Coming Here");
                        isSGGridHide = false;
                    }
                    DisplayLoading(false);
                }

            }
            catch (Exception ex)
            {
                isHide = true;
                isSGGridHide = true;
                // Toaster.Add(ex.Message.ToString(),MatToastType.Danger,"Manual Assign");
                // ToasterService.AddDanger("Manual Assign", ex.Message.ToString());
                Console.WriteLine(ex.Message.ToString());
                Toaster.Add(ex.Message.ToString(), MatToastType.Danger, "Manual Assign");
                DisplayLoading(false);
            }
            finally
            {
                DisplayLoading(false);
                StateHasChanged();
            }
        }

        private async void GetBOTData()
        {
            if (FromDate == null)
            {
                Toaster.Add("Kindly choose From Date", MatToastType.Warning, "Manual Assign");
                DisplayLoading(false);
            }
            else if (ToDate == null)
            {
                Toaster.Add("Kindly choose To Date", MatToastType.Warning, "Manual Assign");
                DisplayLoading(false);
            }
            else if (Convert.ToDateTime(FromDate) > Convert.ToDateTime(ToDate))
            {
                Toaster.Add("To Date should be greater than From date", MatToastType.Warning, "Manual Assign");
                DisplayLoading(false);
            }
            else
            {
                DisplayLoading(false);
                try
                {
                    isSGGridHide = true;
                    DisplayLoading(true);
                    var AuthState = (AuthStateProvider)AuthProvider;
                    _ValidationSGGrid = await ApiHelper.GetUsingMsgPackAsync<GetValidationSGBOTGrid[]>
                    ("/ManualAssign/GetValidationSGBOTGrid/FromDate/" + FromDate + "/ToDate/" + ToDate + "/Schema/" + schemaName, Http, AuthState.Token);

                    if (_ValidationSGGrid.Count() == 0)
                    {
                        isSGGridHide = true;
                        Toaster.Add("No Accounts for the select project group", MatToastType.Danger, "Manual Assign");
                    }
                    else
                    {
                        isSGGridHide = false;
                    }
                    DisplayLoading(false);
                }
                catch (Exception ex)
                {
                    isSGGridHide = true;
                    // Toaster.Add(ex.Message.ToString(),MatToastType.Danger,"Manual Assign");
                    // ToasterService.AddDanger("Manual Assign", ex.Message.ToString());
                    Toaster.Add(ex.Message.ToString(), MatToastType.Danger, "Manual Assign");
                    DisplayLoading(false);
                }
                finally
                {
                    DisplayLoading(false);
                    StateHasChanged();
                }
            }

        }
        private async Task OnFilter(IEnumerable<SearchCriteria> searchCriterias)
        {
            DisplayLoading(true, "Loading");
            string searchValue = string.Empty;
            string Filter = GetFilterValue(searchCriterias, dsSelectedUniqueField[0].FieldResult);
            Filter = Filter != string.Empty ? Filter.Trim() : "EMPTY";
            var data = new GetAccount()
            {
                UploadTemplateID = uploadTemplateID,
                Filter = Filter,
                ProjectGroupID = PGroupID,
                SchemaName = schemaName
            };
            var AuthState = (AuthStateProvider)AuthProvider;
            dsSelectedUniqueField = await ApiHelper.PostUsingMsgPackAsync<FieldAccountMapping[]>("/ManualAssign/GetFieldAccountMapping", data, Http, AuthState.Token);
            if (dsSelectedUniqueField[0].Accountdetails.Count() > 0)
            {
                AccountProperty.Clear();
                AccountProperty = new List<Tuple<PropertyInfo, string>>();
                var type = typeof(GetSearchAndCodeResult);
                foreach (var item in dsSelectedUniqueField[0].FieldResult)
                {
                    AccountProperty.Add(Tuple.Create(type.GetProperty(item.Field), item.DisplayName));
                };
                AccountProperty.Add(Tuple.Create(type.GetProperty("Assigned"), "Assigned"));
                foreach (var item in dsSelectedUniqueField[0].Accountdetails)
                {
                    _Accountinfo = dsSelectedUniqueField[0].Accountdetails;
                };
            }
            else
            {
                Toaster.Add("No Accounts Available", MatToastType.Danger, "Manual Assign");
            }
            DisplayLoading(false);
        }
        private string GetFilterValue(IEnumerable<SearchCriteria> SearchCriterias, IEnumerable<GetFieldResult> uniqueFields)
        {
            string SearchValue = string.Empty;
            string fieldName = string.Empty, dataType = string.Empty;
            List<string> columnList = new List<string>();
            foreach (var sp in SearchCriterias)
            {
                if (sp.Field == "Assigned")
                {
                    fieldName = "Signinname";
                    dataType = "string";

                }
                else
                {
                    fieldName = uniqueFields.Where(exp => exp.DisplayName == sp.Field).Select(exp => exp.Field).FirstOrDefault();
                    dataType = uniqueFields.Where(exp => exp.DisplayName == sp.Field).Select(exp => exp.InputDataType).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(fieldName))
                {
                    // sp.Field = fieldName;
                    string operatorname = GetValueWithOperator(sp.SearchValue, sp.Operator);
                    if (fieldName == "Signinname")
                    {
                        SearchValue = "emp." + fieldName + operatorname;
                    }
                    else if (dataType == "date")
                    {
                        SearchValue = "CONVERT(date,UA." + fieldName + ") = '" + sp.SearchValue + "'";
                    }
                    else
                    {
                        SearchValue = "UA." + fieldName + operatorname;
                    }
                    columnList.Add(SearchValue);
                }
            }
            if (columnList.Count == 1)
            {
                return SearchValue;
            }
            else if (columnList.Count > 1)
            {
                return string.Join(" AND ", columnList.ToArray());
            }
            else
            {
                return SearchValue;
            }
        }
        private string GetValueWithOperator(string searchvalue, string operatorname)
        {
            if (operatorname == "Equal")
            {
                return " = '" + searchvalue + "' ";
            }
            // else if (operatorname == "GreaterThan")
            // {
            //     return " > " + searchvalue + " ";
            // }
            // else if (operatorname == "LesserThan")
            // {
            //     return " < " + searchvalue + " ";
            // }
            // else if (operatorname == "GreaterThanOrEqual")
            // {
            //     return " >= " + searchvalue + " ";
            // }
            // else if (operatorname == "LesserThanOrEqual")
            // {
            //     return " <= " + searchvalue + " ";
            // }
            else if (operatorname == "NotEqual")
            {
                return " <> '" + searchvalue + "' ";
            }
            else if (operatorname == "Contains")
            {
                return " like '%" + searchvalue + "%'";
            }
            else if (operatorname == "StartsWith")
            {
                return " like '" + searchvalue + "%' ";
            }
            else if (operatorname == "EndsWith")
            {
                return " like '%" + searchvalue + "' ";
            }
            else if (operatorname == "NotContains")
            {
                return " not like '%" + searchvalue + "%' ";
            }
            // else if (operatorname == "NotStartsWith")
            // {
            //     return " not like '" + searchvalue + "%' ";
            // }
            // else if (operatorname == "NotEndsWith")
            // {
            //     return " not like '%" + searchvalue + "' ";
            // }
            else
            {
                return " like '%" + searchvalue + "%' ";
            }
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



    }
}