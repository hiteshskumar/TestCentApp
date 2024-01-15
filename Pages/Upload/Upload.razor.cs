using System.Runtime.CompilerServices;
using System.Globalization;
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
using static ChargesWFM.UI.Pages.ProductionUpload;

namespace ChargesWFM.UI.Pages.Upload
{
    public partial class Upload : ComponentBase
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
        private bool isEnabled = false;
        private bool displayLoading;
        private string? progressText { get; set; }
        private UploadTemplate[]? _uploadTemplate;
        private AuthenticatedUser? user;
        private int uploadTemplateID = 0;
        private int projectGroupID = 0;
        public string schemaName = string.Empty;
        private string uploadTemplateName = string.Empty;
        private int ProjectGroupID { get; set; }
        private string? ProjectGroupName { get; set; }
        private bool displayFileLoading;
        private bool displayFileUploading;
        protected Boolean disableUploadBtn = true;
        private Boolean ShowGridUploadError = true;
        private ErrorDetail[]? _uploadedData;
        private Tuple<string, GridSortOrder> sortColumn;
        private UploadProductionExcelFile uploadFile;
        private readonly IList<Models.UploadFileDetails> selectedFiles = new List<Models.UploadFileDetails>();
        private bool IsAppend { get; set; }
        public string? AllocationMethod { get; set; }
        public bool AllocateToSkipAccounts { get; set; }
        public bool AllocateToPreviouslyWorkedAgents { get; set; }
        private string? fileName = string.Empty;
        private string? UploadType = "LINQ";

        private int NumberOfDays = 60;
        private UploadConfiguration? UploadConfigModel = null;
        protected override async Task OnInitializedAsync()
        {
            user = await LocalStorageService.GetAsync<AuthenticatedUser>();
            var projectGroup = await LocalStorageService.GetAsync<NexgenProjectGroups>();
            uploadFile = new UploadProductionExcelFile();
            if (projectGroup != null)
            {
                projectGroupID = projectGroup.ProjectGroupID;
                schemaName = projectGroup.SchemaName;
                await GetInventoryUploadConfiguration(projectGroup);
                await GetUploadTemplates(projectGroup);
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
            var projectGroup = await LocalStorageService.GetAsync<NexgenProjectGroups>();
            uploadFile = new UploadProductionExcelFile();
            if (projectGroup != null)
            {
                projectGroupID = projectGroup.ProjectGroupID;
                schemaName = projectGroup.SchemaName;
            }
            ShowGridUploadError = true;
            _uploadedData = null;
            fileName = string.Empty;
            this.disableUploadBtn = true;
            await GetInventoryUploadConfiguration(pg);
            await GetUploadTemplates(pg);
            this.StateHasChanged();
        }
        public async Task GetUploadTemplates(NexgenProjectGroups projectGroup)
        {
            try
            {
                DisplayLoading(true, "Loading");
                projectGroupID = projectGroup.ProjectGroupID;
                var EmployeeID = user.EmployeeId;
                var AuthState = (AuthStateProvider)AuthProvider;
                _uploadTemplate = await ApiHelper.GetUsingMsgPackAsync<UploadTemplate[]>("/Upload/GetUploadTemplates/EmployeeID/" + EmployeeID + "/ProjectGroupID/" + projectGroup.ProjectGroupID, Http, AuthState.Token);
                if (_uploadTemplate.Count() == 0)
                {
                    Toaster.Add("Error in fetch Upload Template Type", MatToastType.Danger, "Upload");
                }
                else
                {
                    uploadTemplateID = _uploadTemplate[0].UploadTemplateID;
                    uploadTemplateName = _uploadTemplate[0].UploadTemplateName;
                    AllocationMethod = _uploadTemplate[0].AllocationMethod.Trim();
                    AllocateToSkipAccounts = _uploadTemplate[0].AllocateToSkipAccounts;
                    AllocateToPreviouslyWorkedAgents = _uploadTemplate[0].AllocateToPreviouslyWorkedAgents;
                }
                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error in fetch Upload Template Type", MatToastType.Danger, "Upload");
                DisplayLoading(false);
            }
        }
        public async Task GetInventoryUploadConfiguration(NexgenProjectGroups projectGroup)
        {
            try
            {
                DisplayLoading(true, "Loading");
                projectGroupID = projectGroup.ProjectGroupID;
                var AuthState = (AuthStateProvider)AuthProvider;
                UploadConfigModel = await ApiHelper.GetUsingMsgPackAsync<UploadConfiguration>("/Upload/GetInventoryUploadConfiguration/ProjectGroupID/" + projectGroupID, Http, AuthState.Token);
                if (UploadConfigModel != null)
                {
                    UploadType = UploadConfigModel.UploadType;
                    NumberOfDays = UploadConfigModel.NumberOfDays;
                    // UploadType = UploadType;
                    // NumberOfDays = 60;
                }
                else
                {
                    UploadType = UploadType;
                    NumberOfDays = 60;
                }

                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error in fetch Upload Template Type", MatToastType.Danger, "Upload");
                DisplayLoading(false);
            }
        }

        public async Task ChangeUploadTemplateType(ChangeEventArgs e)
        {
            UploadTemplate result = Array.Find<UploadTemplate>(_uploadTemplate, element => element.UploadTemplateName == e.Value?.ToString());
            uploadTemplateID = result.UploadTemplateID;
            uploadTemplateName = result.UploadTemplateName;
            AllocationMethod = result.AllocationMethod.Trim();
            AllocateToSkipAccounts = result.AllocateToSkipAccounts;
            AllocateToPreviouslyWorkedAgents = result.AllocateToPreviouslyWorkedAgents;
            Console.WriteLine(AllocationMethod);
            ShowGridUploadError = true;
            _uploadedData = null;
            await LocalStorageService.SetAsync(result);
        }
        protected async Task btnDownload()
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                var EmployeeID = user.EmployeeId;
                string apiURL = $"Upload/DownloadProductionExcelTemplate/EmployeeID/" + EmployeeID + "/UploadTemplateID/" + uploadTemplateID;
                var dt = new DataTable();
                var columnNames = await ApiHelper.GetUsingMsgPackAsync<IEnumerable<string>>(apiURL, Http, AuthState.Token);
                dt = ToDataTable(columnNames);
                ExcelFeatures excelFeatures = new ExcelFeatures();
                excelFeatures.SheetName = "Data";
                excelFeatures.FileName = uploadTemplateName + ".xlsx";
                await ExcelPackage.DownloadExcel(JSRuntime, excelFeatures, dt);
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error in fetch download template type", MatToastType.Danger, "Upload");
                DisplayLoading(false);
            }
        }
        public DataTable ToDataTable(IEnumerable<string> items)
        {
            var tb = new DataTable();
            foreach (string item in items)
            {
                DataColumn dtColumn = new DataColumn();
                dtColumn.DataType = typeof(String);
                dtColumn.ColumnName = item;
                tb.Columns.Add(dtColumn);
            }
            return tb;
        }
        protected async Task OnChooseFile(InputFileChangeEventArgs e)
        {
            displayFileLoading = true;
            // Get the selected file
            var file = e.File;
            string filename = e.File.Name;
            int lastindex = file.Name.LastIndexOf(".");
            int fileLen = file.Name.Length;
            var extension = filename.Substring(lastindex, fileLen - lastindex);
            if (extension != ".xlsx" && extension != ".xls")
            {
                Toaster.Add("Invalid file uploaded.!", MatToastType.Danger, "Upload");
                displayFileLoading = false;
                return;
            }
            // Check if the file is null then return from the method
            if (file == null)
                return;
            using (var ms = new MemoryStream())
            {
                await file.OpenReadStream().CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                uploadFile.UploadFileBytes = fileBytes;
                fileName = file.Name;
                uploadFile.UploadFileName = Path.GetFileName(file.Name);

                var uploadFileLinq = new Models.UploadFileDetails
                {
                    Name = file.Name,
                    Size = fileBytes.Length,
                    Bytes = fileBytes,
                };
                selectedFiles.Clear();
                selectedFiles.Add(uploadFileLinq);
            }
            displayFileLoading = false;
            disableUploadBtn = false;
            fileName = file.Name;
        }
        public async Task UploadByAPI()
        {
            try
            {
                DisplayLoading(true, "Loading");
                displayFileUploading = true;
                disableUploadBtn = true;
                ShowGridUploadError = true;
                _uploadedData = null;
                var EmployeeID = user!.EmployeeId;
                uploadFile.UploadTemplateID = uploadTemplateID;
                uploadFile.ProjectGroupID = projectGroupID;
                uploadFile.SchemaName = schemaName;
                uploadFile.UploadedById = EmployeeID;
                var token = await LocalStorageService.GetAsync<string>("Token");
                var response = await ApiHelper.PostUsingMsgPackAsync("/Upload/UploadProductionExcelFile", uploadFile, Http, token);
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
                Toaster.Add("Error occured in upload", MatToastType.Danger, "Upload");
                DisplayLoading(false);
            }
            finally
            {
                uploadFile.UploadFileBytes = new byte[0];
                fileName = string.Empty;
                uploadFile.UploadFileName = string.Empty;
                StateHasChanged();
            }
        }

        public async Task UploadByLinq()
        {
            try
            {
                DisplayLoading(true, "Loading");
                displayFileUploading = true;
                disableUploadBtn = true;
                ShowGridUploadError = true;
                _uploadedData = null;
                var EmployeeID = user!.EmployeeId;
                // uploadFile.UploadTemplateID = uploadTemplateID;
                // uploadFile.ProjectGroupID = projectGroupID;
                // uploadFile.SchemaName = schemaName;
                // uploadFile.UploadedById = EmployeeID;

                var uploadFile = new ProductionUploadFile
                {
                    Files = selectedFiles.AsEnumerable(),
                    ProjectGroupId = projectGroupID,
                    SchemaName = schemaName,
                    UploadedBy = EmployeeID,
                    UploadTemplateID = uploadTemplateID,
                    IsAppendAccounts = IsAppend,
                    AllocationMethod = AllocationMethod,
                    AllocateToSkipAccounts = AllocateToSkipAccounts,
                    AllocateToPreviouslyWorkedAgents = AllocateToPreviouslyWorkedAgents,
                    NumberOfDays = NumberOfDays
                };
                var token = await LocalStorageService.GetAsync<string>("Token");
                var response = await ApiHelper.PostUsingMsgPackAsync("/Upload/InventoryUpload", uploadFile, Http, token);
                var stream = await response.Content.ReadAsStreamAsync();
                var uploadSuccessConfirmation = MessagePackSerializer.Deserialize<Reports>(stream);

                if (uploadSuccessConfirmation.StatusID == 1)
                {
                    // if(uploadSuccessConfirmation.ErrorDetail.Count() > 0)
                    // {   
                    //     ShowGridUploadError = false;
                    //     _uploadedData = uploadSuccessConfirmation.ErrorDetail.ToArray();
                    //     var ErrorDetail = uploadSuccessConfirmation.ErrorDetail.ToList()[0];
                    // }
                    if (uploadSuccessConfirmation.NoOfRowsAffected != 0)
                    {
                        Toaster.Add(uploadSuccessConfirmation.NoOfRowsAffected + " " + uploadSuccessConfirmation.Status, MatToastType.Success, "Success");
                    }

                }
                if (uploadSuccessConfirmation.StatusID == -1 || (uploadSuccessConfirmation.StatusID == 1 && uploadSuccessConfirmation.ErrorDetail.Count() > 0))
                {

                    if (uploadSuccessConfirmation.ErrorDetail.Count() > 0)
                    {
                        ShowGridUploadError = false;
                        _uploadedData = uploadSuccessConfirmation.ErrorDetail.ToArray();
                        var ErrorDetail = uploadSuccessConfirmation.ErrorDetail.ToList()[0];
                    }
                    if (uploadSuccessConfirmation.NoOfRowsAffected == 0)
                    {
                        Toaster.Add(uploadSuccessConfirmation.NoOfRowsAffected + " " + uploadSuccessConfirmation.Status, MatToastType.Danger, "Upload");
                    }
                }
                disableUploadBtn = false;
                displayFileUploading = false;
                selectedFiles.Clear();
                DisplayLoading(false);

            }
            catch (Exception ex)
            {
                Toaster.Add("Error occured in upload", MatToastType.Danger, "Upload");
                DisplayLoading(false);
            }
            finally
            {
                uploadFile.UploadFileBytes = new byte[0];
                fileName = string.Empty;
                uploadFile.UploadFileName = string.Empty;
                displayFileLoading = false;
                selectedFiles.Clear();
                StateHasChanged();
            }
        }
        public void CheckboxClicked(bool checkedValue)
        {
            uploadFile.ISAppend = checkedValue;
            IsAppend = checkedValue;
        }
    }
    [MessagePackObject]
    public class ProductionUploadFile
    {
        [Key(0)]
        public IEnumerable<Models.UploadFileDetails> Files { get; set; }
        [Key(1)]
        public bool CanSystemResolve { get; set; }
        [Key(2)]
        public bool UnassignAccounts { get; set; }
        [Key(3)]
        public int ProjectGroupId { get; set; }
        [Key(4)]
        public int PlacementId { get; set; }
        [Key(5)]
        public int UploadedBy { get; set; }
        [Key(6)]
        public string? SchemaName { get; set; }
        [Key(7)]
        public bool IsAppendAccounts { get; set; }
        [Key(8)]
        public int UploadTemplateID { get; set; }

        [Key(9)]
        public string? AllocationMethod { get; set; }

        [Key(10)]
        public bool AllocateToSkipAccounts { get; set; }

        [Key(11)]
        public bool AllocateToPreviouslyWorkedAgents { get; set; }
        [Key(12)]
        public int NumberOfDays { get; set; }

    }
}