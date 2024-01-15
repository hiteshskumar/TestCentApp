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

namespace ChargesWFM.UI.Pages.ProductivityReport
{
    public partial class ProductivityReport
    {
        private bool isEnabled = false;
        private bool displayLoading;
        private bool displayFileLoading;
        private bool displayMessageScreen;
        private bool disableUploadButtons = true;
        private string? progressText { get; set; }

        [Inject]
        HttpClient Http { get; set; }
        [Inject]
        AuthenticationStateProvider? AuthProvider { get; set; }
        [Inject]
        ILocalStorageService? LocalStorageService { get; set; }

        [Inject]
        IToasterService? ToasterService { get; set; }

        [Inject]
        IMatToaster? Toaster { get; set; }
        private int EmpID { get; set; }
        private int PGroupID { get; set; }
        private string? schemaName { get; set; }
        private string? ProjectGroupName { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public EventCallback<DateTime?> DateSelected { get; set; }
        private IEnumerable<GetProductivityReportResults> dsresultset = Enumerable.Empty<GetProductivityReportResults>();
        List<string> downloadExcelHeaders = new List<string>();
        List<string> downloadExcelHeadersDisplay = new List<string>();
        private IEnumerable<ResultSet> dsExceldownload = Enumerable.Empty<ResultSet>();
        protected override async Task OnInitializedAsync()
        {
            var user = await LocalStorageService.GetAsync<AuthenticatedUser>();
            EmpID = user.EmployeeId;

            var projectGroup = await LocalStorageService.GetAsync<NexgenProjectGroups>();
            PGroupID = projectGroup.ProjectGroupID;
            schemaName = projectGroup.SchemaName;
        }
        protected async Task OnProjectGroupChanged(NexgenProjectGroups pg)
        {
            this.PGroupID = pg.ProjectGroupID;
            this.ProjectGroupName = pg.ProjectGroupName;
            this.schemaName = pg.SchemaName;

            this.StateHasChanged();
        }
        private void DisplayLoading(bool display, string? progressMessage = null)
        {
            displayLoading = display;
            progressText = progressMessage ?? string.Empty;
            StateHasChanged();
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
        private async Task btnDownload()
        {
            DisplayLoading(true, "Loading");
            if (FromDate == null)
            {
                Toaster.Add("Kindly choose From Date", MatToastType.Warning, "Inventory Dashboard");
                DisplayLoading(false);
            }
            else if (ToDate == null)
            {
                Toaster.Add("Kindly choose To Date", MatToastType.Warning, "Inventory Dashboard");
                DisplayLoading(false);
            }
            else if (Convert.ToDateTime(FromDate) > Convert.ToDateTime(ToDate))
            {
                Toaster.Add("To Date should be greater than From date", MatToastType.Warning, "Inventory Dashboard");
                DisplayLoading(false);
            }
            else
            {
                DisplayLoading(false);
                try
                {
                    DisplayLoading(true);
                    var AuthState = (AuthStateProvider)AuthProvider;

                    dsresultset = await ApiHelper.GetUsingMsgPackAsync<GetProductivityReportResults[]>
                    ("/ProductivityReport/GetProductivityReportResult/EmployeeID/" + EmpID +
                    "/FromDate/" + FromDate + "/ToDate/" + ToDate + "/ProjectGroupID/" + PGroupID, Http, AuthState.Token);
                    if (dsresultset.ToList()[0].ResultSet.Count() > 0)
                    {
                        DisplayLoading(true);

                        downloadExcelHeaders = dsresultset.ToList()[0].ColumnSet;

                        downloadExcelHeadersDisplay = dsresultset.ToList()[0].DisplayColumn;

                        dsExceldownload = dsresultset.ToList()[0].ResultSet;

                        downloadExcelHeadersDisplay.AddRange(downloadExcelHeaders);

                        var type = typeof(GetProductivityReportResults);
                        var bytes = await ExcelPackage.GenerateTemplateAsync1(downloadExcelHeaders.ToArray(),
                        downloadExcelHeadersDisplay.ToArray(), dsresultset.ToList()[0].ResultSet);
                        await BlazorDownloadFileService.DownloadFile("ProductivityReport_" + ProjectGroupName + ".xlsx", bytes,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    }
                    else
                    {
                        Toaster.Add("No Reports on Selected Date", MatToastType.Info, "Productivity Report");
                    }


                }
                catch (Exception ex)
                {
                    DisplayLoading(false);
                    Toaster.Add("Error occured while loading Global DashBoard" + ex.Message, MatToastType.Danger, "Productivity Report");
                }
                finally
                {
                    DisplayLoading(false);
                    StateHasChanged();
                }
            }
        }
    }
}