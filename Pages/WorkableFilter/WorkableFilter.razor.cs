using System;
using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using ChargesWFM.UI.Components.GridComponent;
using System.Net.Http.Headers;
using ChargesWFM.UI.Models;
using ChargesWFM.UI.Services;
using Microsoft.JSInterop;
using MatBlazor;
using System.Data;
using Newtonsoft.Json;

namespace ChargesWFM.UI.Pages.WorkableFilter
{
    public partial class WorkableFilter
    {
        	private WorkableErrorDetails[]? _uploadedData;	
            private Tuple<string, GridSortOrder> sortColumn;	
            protected Boolean disableUploadBtn = true;	
            [Inject]	
            HttpClient Http { get; set; }	
            [Inject]	
            IJSRuntime JSRuntime { get; set; }	
            [Inject]	
            AuthenticationStateProvider AuthProvider { get; set; }	
            [Inject]	
            ILocalStorageService LocalStorageService { get; set; }	
            [Inject]	
            IMatToaster Toaster { get; set; }	
            [Inject]	
            IToasterService? ToasterService { get; set; }	
            MultipartFormDataContent form;
            ByteArrayContent fileContent;	
            protected string apiName = string.Empty;	
            protected string isNewUpload = "true";	
            protected string serverID = "1";	
            public int UploadedBy;	
            public Boolean loadspinner=false;	
            private bool displayLoading;	
            public int EmpID = 0;	
            private string? progressText { get; set; }	
            private int projectGroupID = 0;		
            private string? ProjectGroupName { get; set; }	
            public string schemaName = string.Empty;      	
            private DownloadWorkableFilterdetails[] DownloadExcel;
            private bool isHide = true;
        protected override async Task OnInitializedAsync()
        {
            var user = await LocalStorageService.GetAsync<AuthenticatedUser>();
            EmpID = user.EmployeeId;
            var projectGroup = await LocalStorageService.GetAsync<NexgenProjectGroups>();
            projectGroupID = projectGroup.ProjectGroupID;
            schemaName = projectGroup.SchemaName;
            // projectGroupID  = 6310;         
            // schemaName = "NexgenBannerHealthFC.BannerHealth.";
            // projectGroupID  = 6297;     
            // schemaName = "NexgenParallon.ParallonIP.";
            await Task.Run(Loaduploadfilter);
        }
        protected async Task OnProjectGroupChanged(NexgenProjectGroups pg)
        {
            this.projectGroupID = pg.ProjectGroupID;
            schemaName = pg.SchemaName;      
            isHide = true;
            // projectGroupID  = 6297;     
            // schemaName = "NexgenParallon.ParallonIP.";
            this.ProjectGroupName = pg.ProjectGroupName;
            this.StateHasChanged();
        }
        private void DisplayLoading(bool display, string? progressMessage = null)
        {
            displayLoading = display;
            progressText = progressMessage ?? string.Empty;
            StateHasChanged();
        }
        private void Loaduploadfilter()
        {
            System.Threading.Thread.Sleep(2000);
        }
        protected async Task btnDownload()
        {
            // loadspinner=true; 	
            // schemaName = "NexgenBannerHealthFC.BannerHealth.";
            isHide = true;
            Guid l_guidFileID = Guid.NewGuid();
            var AuthState = (AuthStateProvider)AuthProvider;
            try
            {
                // var uploadTemplateName ="WorkableFilter";	
                // string apiURL = $"WorkableFilter/DownloadWorkableExcelTemplate/EmployeeID/" + EmpID + "/ProjectGroupID/" + ProjectGroupID +"/SchemaName" +schemaName;	
                // var dt = new DataTable();          	
                // var columnNames = await ApiHelper.GetUsingMsgPackAsync<IEnumerable<string>>(apiURL, Http, AuthState.Token);	
                // dt = ToDataTable(columnNames);	
                // ExcelFeatures excelFeatures = new ExcelFeatures();	
                // excelFeatures.SheetName = "Data";	
                // excelFeatures.FileName = uploadTemplateName + l_guidFileID + ".xlsx";	
                // var bytes = await ExcelPackage.GenerateTemplateAsync(dt);	
                // await BlazorDownloadFileService.DownloadFile(excelFeatures.FileName, bytes,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");	
                // loadspinner=false;	
                DownloadExcel = await
                                ApiHelper.GetUsingMsgPackAsync<DownloadWorkableFilterdetails[]>
                                ("/WorkableFilter/DownloadWorkableExcelTemplate/EmployeeID/" + EmpID +
                                "/ProjectGroupID/" + projectGroupID + "/SchemaName/"+schemaName,
                                Http, AuthState.Token);
                // var type = typeof(GetAccountDetailsResult);	
                
                if(DownloadExcel[0].result.ToList()[0].ErrorMessage == "Workable Filter Not Configured"){
                    // ToasterService.AddDanger("Workable Filter","Workable Filter Not Configured");
                    Toaster.Add("Workable Filter Not Configured", MatToastType.Info, "Workable Filter");
                }
                else{
                    List<string> ExcelHeader = new List<string>();
                    List<string> ExcelHeaderDisplay = new List<string>();
                    foreach (var item in DownloadExcel[0].UniqueColumns)
                    {
                        ExcelHeader.Add(item.Field);
                        ExcelHeaderDisplay.Add(item.DisplayName);
                    };
                    ExcelHeader.Add("SubClient");
                    ExcelHeaderDisplay.Add("Sub Client");
                    ExcelHeader.Add("BillableGroup");
                    ExcelHeaderDisplay.Add("Billable Group");
                    Console.WriteLine("result-" + ExcelHeaderDisplay[0]);
                    var bytes = await ExcelPackage.GenerateTemplateAsync1(ExcelHeader.ToArray(), ExcelHeaderDisplay.ToArray(), DownloadExcel[0].result);
                    await BlazorDownloadFileService.DownloadFile("WorkableFilterUploadTemplate.xlsx", bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ToasterService.AddDanger("Workable Filter", ex.Message.ToString());
            }
        }    
            private async Task UploadExcelData()
            {
               
                form.Add(new StringContent(serverID.ToString()), "SheetName");
                form.Add(new StringContent(projectGroupID.ToString()), "ProjectGroupID");
                form.Add(new StringContent(schemaName), "SchemaName");
                form.Add(new StringContent(EmpID.ToString()), "EmployeeID");
                // WorkableErrorDetails statusMessageResponse = new WorkableErrorDetails();
                HttpResponseMessage response = await Http.PostAsync("/WorkableFilter/uploadworkablefiltertemplate", form);
                var responseContent = await response.Content.ReadAsStringAsync();
                var griddata2 = JsonConvert.DeserializeObject<UploadWorkableFilterdetails>(responseContent);
                //_uploadedData = griddata2.ToArray();
                Console.WriteLine("Result - "+griddata2.Success);
                Console.WriteLine("Result - "+griddata2.UploadResult);
                Console.WriteLine(responseContent);
                if(griddata2.Success != null && griddata2.Success.Status == "Uploaded Successfully" )
                {
                    Toaster.Add("Excel data uploaded successfully", MatToastType.Success, "WorkableFiler Data Upload");
                }
                if(griddata2.UploadResult.Count() > 0)
                {
                    isHide = false;
                    //Toaster.Add(_uploadedData[0].Errordetails.ToString(), MatToastType.Danger, "WorkableFilter Data Not Upload");
                    _uploadedData = griddata2.UploadResult.ToArray();
                }
                await InvokeAsync(() => StateHasChanged()).ConfigureAwait(false);
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

            private async Task OnInputFileChange(InputFileChangeEventArgs e)
            {
                form =  new MultipartFormDataContent();
                loadspinner = true;
                var user = await LocalStorageService.GetAsync<AuthenticatedUser>();
                //using var content = new MultipartFormDataContent();
                disableUploadBtn = false;
                foreach (var file in e.GetMultipleFiles(1))
                {
                    if (file.Size > int.Parse("25000000"))
                    {
                        Toaster.Add($"File size is more than 25 MB", MatToastType.Warning);
                        await JSRuntime.InvokeVoidAsync("clearUploadedFile");
                    }
                    disableUploadBtn = false;
                    using (var ms = new MemoryStream())
                    {
                        await file.OpenReadStream().CopyToAsync(ms);
                        var fileBytes = ms.ToArray();

                        fileContent = new ByteArrayContent(fileBytes);
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                        form.Add(fileContent, "file", Path.GetFileName(file.Name));
                        loadspinner = false;
                    }
                }
            }
            private async Task OnClickInputFile()
            {
                await JSRuntime.InvokeVoidAsync("clearUploadedFile");
                disableUploadBtn = true;
            }
        }
    }



