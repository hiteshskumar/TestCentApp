using System;
using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components.Authorization;
using ChargesWFM.UI.Components.GridComponent;
using ChargesWFM.UI.Models;
using ChargesWFM.UI.Services;
using ChargesWFM.UI.Pages;
using Microsoft.JSInterop;
using MatBlazor;
using System.Data;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using MessagePack;

namespace ChargesWFM.UI.Pages.EmployeeCategoryMapping
{
    public partial class EmployeeCategoryMapping : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthProvider { get; set; }
        [Inject]
        ILocalStorageService LocalStorageService { get; set; }
        [Inject]
        IMatToaster Toaster { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        private bool displayLoading;
        private AuthenticatedUser? user;
        private string? progressText { get; set; }
        public string? SchemaName { get; set; }
        private int ProjectGroupID { get; set; }
        private string? ProjectGroupName { get; set; }
        private bool displayFileLoading;
        private bool displayFileUploading;
        protected Boolean disableUploadBtn = true;
        private UploadProductionExcelFile uploadFile;
        private Boolean ShowGridUploadError = true;
        private Boolean ShowEmployeeCategoryMappingGrid = true;
        private ErrorDetail[]? _uploadedData;
        private EmployeeCategoryMappings[]? _employeeCategoryMappings;
        private Tuple<string, GridSortOrder> sortColumn;
        private List<DropDownList> _employeeCategory = new List<DropDownList>();
        private List<DropDownList> _employeeLoginStatus = new List<DropDownList>();
        public EventCallback<IEnumerable<EmployeeCategoryMappings>> BulkCheckedChanged { get; set; }
        public EventCallback<IEnumerable<EmployeeCategoryMappings>> ItemsChanged { get; set; }
        private List<int> EmployeeID = new List<int>();
        private string? fileName = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            user = await LocalStorageService.GetAsync<AuthenticatedUser>();
            var projectGroup = await LocalStorageService.GetAsync<NexgenProjectGroups>();
            uploadFile = new UploadProductionExcelFile();
            if (projectGroup != null && user.Screens.Any(x => x.Name == "Employee Category Mapping"))
            {
                ProjectGroupID = projectGroup.ProjectGroupID;
                SchemaName = projectGroup.SchemaName;
                await GetEmployeeCategoryMaster();
                await GetEmployeeCategoryLoginStatusMaster();
                await GetEmployeeCategoryMappings();
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
            this.ProjectGroupID = pg.ProjectGroupID;
            this.ProjectGroupName = pg.ProjectGroupName;
            ShowGridUploadError = true;
            ShowEmployeeCategoryMappingGrid = true;
            _uploadedData = null;
            _employeeCategoryMappings = null;
            if (user.Screens.Any(x => x.Name == "Employee Category Mapping"))
            {
                await GetEmployeeCategoryMaster();
                await GetEmployeeCategoryLoginStatusMaster();
                await GetEmployeeCategoryMappings();
            }
            this.StateHasChanged();
        }
        public async Task GetEmployeeCategoryMappings()
        {
            try
            {
                DisplayLoading(true, "Loading");
                var EmployeeID = user.EmployeeId;
                var AuthState = (AuthStateProvider)AuthProvider;
                string apiURL = $"EmployeeCategoryMapping/GetEmployeeCategoryMappings/EmployeeID/" + EmployeeID + "/ProjectGroupID/" + ProjectGroupID;
                var EmployeeCategoryMappings = await ApiHelper.GetUsingMsgPackAsync<IEnumerable<EmployeeCategoryMappings>>(apiURL, Http, AuthState.Token);
                if (EmployeeCategoryMappings.Count() == 0)
                {
                    Toaster.Add("Error in fetch Employee Category Mapping details", MatToastType.Danger, "EmployeeCategoryMapping");
                }
                else
                {
                    _employeeCategoryMappings = EmployeeCategoryMappings.ToArray();
                }

                ShowEmployeeCategoryMappingGrid = false;
                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error in fetch Employee Category Mapping details", MatToastType.Danger, "Employee Category Mapping");
                DisplayLoading(false);
            }
        }
        public async Task GetEmployeeCategoryMaster()
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                string apiURL = $"EmployeeCategoryMapping/GetEmployeeCategoryMaster";
                var EmployeeCategoryMaster = await ApiHelper.GetUsingMsgPackAsync<IEnumerable<DropDownList>>(apiURL, Http, AuthState.Token);
                if (EmployeeCategoryMaster.Count() == 0)
                {
                    Toaster.Add("Error in fetch Employee Category Master details", MatToastType.Danger, "EmployeeCategoryMapping");
                }
                else
                {
                    _employeeCategory = EmployeeCategoryMaster.ToList();
                }

                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error in fetch Employee Category Mapping details", MatToastType.Danger, "Employee Category Mapping");
                DisplayLoading(false);
            }
        }
        public async Task GetEmployeeCategoryLoginStatusMaster()
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                string apiURL = $"EmployeeCategoryMapping/GetEmployeeCategoryLoginStatusMaster";
                var EmployeeCategoryLoginStatusMaster = await ApiHelper.GetUsingMsgPackAsync<IEnumerable<DropDownList>>(apiURL, Http, AuthState.Token);
                if (EmployeeCategoryLoginStatusMaster.Count() == 0)
                {
                    Toaster.Add("Error in fetch Employee Login Status details", MatToastType.Danger, "EmployeeCategoryMapping");
                }
                else
                {
                    _employeeLoginStatus = EmployeeCategoryLoginStatusMaster.ToList();
                }

                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error in fetch Employee Login Status details", MatToastType.Danger, "Employee Category Mapping");
                DisplayLoading(false);
            }
        }
        public async Task ChangeEmployeeCategory(int ReportingEmployeeID, int Value)
        {
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
            _employeeCategoryMappings.First(e => e.ReportingEmployeeID == ReportingEmployeeID).EmployeeCategory = Value;
        }
        public async Task ChangeEmployeeLoginStatus(int ReportingEmployeeID, int Value)
        {
            await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
            _employeeCategoryMappings.First(e => e.ReportingEmployeeID == ReportingEmployeeID).LoginStatus = Value;
        }
        public async Task UpdateEmployeeCategory()
        {
            try
            {
                DisplayLoading(true, "Loading");
                ShowGridUploadError = true;
                _uploadedData = null;
                UpdateEmployeeCategoryMappingDetails updateEmployeeCategory = new UpdateEmployeeCategoryMappingDetails();
                updateEmployeeCategory.EmployeeCategoryMappingDetails = _employeeCategoryMappings;
                updateEmployeeCategory.ProjectGroupID = this.ProjectGroupID;
                updateEmployeeCategory.EmployeeID = user.EmployeeId;
                var token = await LocalStorageService.GetAsync<string>("Token");
                string apiURL = "/EmployeeCategoryMapping/UpdateEmployeeCategoryMappingDetails";
                var response = await ApiHelper.PostUsingMsgPackAsync(apiURL, updateEmployeeCategory, Http, token);
                var stream = await response.Content.ReadAsStreamAsync();
                var uploadSuccessConfirmation = MessagePackSerializer.Deserialize<UploadSuccessConfirmation>(stream);
                if (uploadSuccessConfirmation.Confirmation != null && uploadSuccessConfirmation.Confirmation.ToList().Count != 0)
                {
                    Toaster.Add(uploadSuccessConfirmation.Confirmation.ToList()[0].Status, MatToastType.Success, "Upload");
                    await GetEmployeeCategoryMappings();
                    EmployeeID.Clear();
                }

                if (uploadSuccessConfirmation.ErrorDetail != null && uploadSuccessConfirmation.ErrorDetail.ToList().Count != 0)
                {
                    ShowGridUploadError = false;
                    _uploadedData = uploadSuccessConfirmation.ErrorDetail.ToArray();
                    var ErrorDetail = uploadSuccessConfirmation.ErrorDetail.ToList()[0];
                    Toaster.Add(ErrorDetail.Error + ErrorDetail.Description, MatToastType.Danger, "Upload");
                }

                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error in saving employee category", MatToastType.Danger, "Employee Category Mapping");
                DisplayLoading(false);
            }
        }
        protected async Task btnDownload()
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                var EmployeeID = user.EmployeeId;
                string apiURL = $"EmployeeCategoryMapping/GetEmployeeCategoryMappingTemplate/EmployeeID/" + EmployeeID + "/ProjectGroupID/" + ProjectGroupID;
                var columnNames = await ApiHelper.GetUsingMsgPackAsync<IEnumerable<EmployeeCategoryMappingTemplate>>(apiURL, Http, AuthState.Token);
                if (columnNames.Count() > 0)
                {
                    var bytes = await ExcelPackage.GenerateTemplateAsync(columnNames);
                    await BlazorDownloadFileService.DownloadFile("EmployeeCategoryMapping.xlsx", bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
                else
                {
                    Toaster.Add("Unable to download the excel fiel", MatToastType.Danger, "EmployeeCategoryMapping");
                }

                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error in Download Employee Category Mapping", MatToastType.Danger, "Employee Category Mapping");
                DisplayLoading(false);
            }
        }
        protected async Task OnChooseFile(InputFileChangeEventArgs e)
        {
            displayFileLoading = true;
            // Get the selected file
            var file = e.File;

            // Check if the file is null then return from the method
            if (file == null)
                return;
            using (var ms = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                uploadFile.UploadFileBytes = fileBytes;
                uploadFile.UploadFileName = Path.GetFileName(file.Name);
                fileName = file.Name;
            }

            displayFileLoading = false;
            disableUploadBtn = false;
        }
        public async Task UploadTemplate()
        {
            try
            {
                DisplayLoading(true, "Loading");
                displayFileUploading = true;
                disableUploadBtn = true;
                ShowGridUploadError = true;
                _uploadedData = null;
                uploadFile.ProjectGroupID = ProjectGroupID;
                uploadFile.SchemaName = SchemaName;
                uploadFile.UploadedById = user.EmployeeId;
                var token = await LocalStorageService.GetAsync<string>("Token");
                var response = await ApiHelper.PostUsingMsgPackAsync("/EmployeeCategoryMapping/UploadEmployeeCategoryMappingExcelFile", uploadFile, Http, token);
                var stream = await response.Content.ReadAsStreamAsync();
                var uploadSuccessConfirmation = MessagePackSerializer.Deserialize<UploadSuccessConfirmation>(stream);
                if (uploadSuccessConfirmation.Confirmation != null && uploadSuccessConfirmation.Confirmation.ToList().Count != 0)
                {
                    Toaster.Add(uploadSuccessConfirmation.Confirmation.ToList()[0].Status, MatToastType.Success, "Upload");
                }

                if (uploadSuccessConfirmation.ErrorDetail != null && uploadSuccessConfirmation.ErrorDetail.ToList().Count != 0)
                {
                    ShowGridUploadError = false;
                    _uploadedData = uploadSuccessConfirmation.ErrorDetail.ToArray();
                    var ErrorDetail = uploadSuccessConfirmation.ErrorDetail.ToList()[0];
                    Toaster.Add(ErrorDetail.Error + ErrorDetail.Description, MatToastType.Danger, "Upload");
                }

                disableUploadBtn = false;
                displayFileUploading = false;
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error in Upload Employee Category Mapping", MatToastType.Danger, "Employee Category Mapping");
                DisplayLoading(false);
            }
        }
        private Task OnCheckedChanged(Tuple<EmployeeCategoryMappings, bool> args)
        {
            args.Item1.IsChecked = args.Item2;
            if (args.Item2)
            {
                if (!EmployeeID.Contains(args.Item1.ReportingEmployeeID))
                    EmployeeID.Add(args.Item1.ReportingEmployeeID);
            }
            else
            {
                if (EmployeeID.Contains(args.Item1.ReportingEmployeeID))
                    EmployeeID.Remove(args.Item1.ReportingEmployeeID);
            }

            return ItemsChanged.InvokeAsync();
        }
        private Task OnBulkCheckedChanged(Tuple<IEnumerable<EmployeeCategoryMappings>, bool> args)
        {
            foreach (var item in args.Item1)
            {
                item.IsChecked = args.Item2;
                if (args.Item2)
                {
                    if (!EmployeeID.Contains(item.ReportingEmployeeID))
                        EmployeeID.Add(item.ReportingEmployeeID);
                }
                else
                {
                    if (EmployeeID.Contains(item.ReportingEmployeeID))
                        EmployeeID.Remove(item.ReportingEmployeeID);
                }
            }

            return BulkCheckedChanged.InvokeAsync(args.Item1);
        }
    }
}