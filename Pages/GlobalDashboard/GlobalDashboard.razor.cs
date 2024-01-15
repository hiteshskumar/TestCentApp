using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ChargesWFM.UI.Components.GridComponent;
using ChargesWFM.UI.Models;
using ChargesWFM.UI.Services;
using MatBlazor;
using System.Data;
using BlazorDownloadFile;
using ChargesWFM.UI.Components;
using System.IO;
using System.Collections.Generic;
using OfficeOpenXml;



namespace ChargesWFM.UI.Pages.GlobalDashboard

{
    public partial class GlobalDashboard
    {
        [Inject]
        HttpClient? Http { get; set; }
        [Inject]
        AuthenticationStateProvider? AuthProvider { get; set; }
        [Inject]
        ILocalStorageService? LocalStorageService { get; set; }
        [Inject]
        ILogger<GlobalDashboard>? Logger { get; set; }
        [Inject]
        IMatToaster? Toaster { get; set; }
        [Inject]
        IBlazorDownloadFileService? BlazorDownloadFileService { get; set; }
        private bool displayLoading;
        private string? progressText { get; set; }
        private bool isHide = true;
        private bool isProductionHide = false;
        private bool isBacklogHide = false;
        private bool isEfficiencyHide = false;
        private Tuple<string, GridSortOrder>? sortColumn;
        public int EmpID = 0;
        public int PGroupID = 0;
        private string projectgroup;
        public string? schemaName;
        private IEnumerable<GetProcessclientResults> dsProcessclient;
        private List<string>? selectedProcessID = new List<string>();
        private List<string>? selectedProcessclient { get; set; }
        public List<GetGlobalDashboardResult> _GlobalData = new List<GetGlobalDashboardResult>();
        private GlobalProjectGroupsSummary[] CompleteProjectwiseSummary;
        private List<ParentDashboardResult> ProductionDataParent = new List<ParentDashboardResult>();
        private IEnumerable<EmployeewiseProductionDashboardResult> EmployeeProductionDetails = Enumerable.Empty<EmployeewiseProductionDashboardResult>();
        private IEnumerable<EmployeewiseBacklogDashboardResult> EmployeeBacklogDetails = Enumerable.Empty<EmployeewiseBacklogDashboardResult>();
        private IEnumerable<EmployeeWiseEfficiencyDashboardResult> EmployeeEfficiencyDetails = Enumerable.Empty<EmployeeWiseEfficiencyDashboardResult>();
        private DialogNew modal;
        private bool IsDownload = true;
        protected override async Task OnInitializedAsync()
        {
            var user = await LocalStorageService.GetAsync<AuthenticatedUser>();
            EmpID = user.EmployeeId;
            var projectGroup = await LocalStorageService.GetAsync<NexgenProjectGroups>();
            this.PGroupID = projectGroup.ProjectGroupID;
            this.schemaName = projectGroup.SchemaName;
            _GlobalData.Clear(); isHide = true; IsDownload = true;
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
            _GlobalData.Clear(); isHide = true;
            await GetProcessClient(PGroupID);
            this.StateHasChanged();
        }

        public async Task GetProcessClient(int PGroupID)
        {

            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                dsProcessclient = await ApiHelper.GetUsingMsgPackAsync<GetProcessclientResults[]>($"GlobalDashboard/GetProcessClients/EmployeeID/" + EmpID, Http, AuthState.Token);
                selectedProcessclient = dsProcessclient.Select(x => x.ProcessID.ToString()).ToList();
                _GlobalData.Clear();
                isHide = true;
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                // Logger.LogError(ex, ex.Message);
                Toaster.Add("Error occured while loading Process Client", MatToastType.Danger, "Global Dashboard");
                DisplayLoading(false);
            }
            finally
            {
                StateHasChanged();
                DisplayLoading(false);
            }
        }

        private async void GetRecords()
        {
            await FetchGlobalDashboardSummary();
        }

        private async void DownLoad()
        {
            try
            {
                DisplayLoading(true, "Loading");
                string ProjectGroupID = string.Join(',', selectedProcessclient);
                var AuthState = (AuthStateProvider)AuthProvider;
                string url = "/GlobalDashboard/GetGlobalExelDatasource/EmployeeID/" + EmpID + "/ProjectGroupID/" + ProjectGroupID;
                var DownloadSummary = await
                ApiHelper.GetUsingMsgPackAsync<GetGlobalDashboardDownloadResult[]>(url, Http, AuthState.Token);

                var bytes = ExcelService.GenerateGlobalDashboardExcelWorkbook(DownloadSummary);
                await BlazorDownloadFileService.DownloadFile("GlobalDashboardSummary.xlsx", bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                Toaster.Add("Error occured while Download Global DashBoard Summary", MatToastType.Danger, "Global Dashboard");
                DisplayLoading(false);
            }
            finally
            {
                StateHasChanged();
                DisplayLoading(false);
            }

        }
        public async Task FetchGlobalDashboardSummary()
        {
            if (selectedProcessclient.Any())
            {
                DisplayLoading(true, "Loading");
                string ProjectGroupID = string.Join(',', selectedProcessclient);
                var AuthState = (AuthStateProvider)AuthProvider;
                string url = "/GlobalDashboard/GetProjectGroupwisecountResult/EmployeeID/" + EmpID + "/ProjectGroupID/" + ProjectGroupID;
                CompleteProjectwiseSummary = await
                ApiHelper.GetUsingMsgPackAsync<GlobalProjectGroupsSummary[]>(url, Http, AuthState.Token);

                _GlobalData.Clear();
                ProductionDataParent = CompleteProjectwiseSummary[0].ProductionParentResult.ToList();
                IsDownload = false;
                isHide = false;
                try
                {
                    foreach (var item in selectedProcessclient)
                    {
                        _GlobalData.Add(new GetGlobalDashboardResult()
                        {
                            DisplaySubGrid = false,
                            projectgroup = dsProcessclient.Where(e => e.ProcessID.ToString() == item).Select(exp => exp.Client).FirstOrDefault(),
                            ProductionDataCountResult = ProductionDataParent.Where(parent => parent.Projectgroupid.ToString() == item).ToList(),
                        });
                        string? Client = dsProcessclient.Where(e => e.ProcessID.ToString() == item).Select(exp => exp.Client).FirstOrDefault();
                    }
                    DisplayLoading(false);
                }
                catch (Exception ex)
                {
                    // isHide = true;
                    Logger.LogError(ex, ex.Message);
                    Toaster.Add("Error occured while loading Global DashBoard", MatToastType.Danger, "Global Dashboard");
                    DisplayLoading(false);
                }
                finally
                {
                    StateHasChanged();
                    DisplayLoading(false);
                }

            }
            else
            {
                IsDownload = true;
                Toaster.Add("Select Process Clients", MatToastType.Danger, "Global Dashboard");
            }
        }
        private Task DisplaySubGrid(GetGlobalDashboardResult item)
        {
            item.DisplaySubGrid = !item.DisplaySubGrid;
            StateHasChanged();

            if (item.DisplaySubGrid == true)
            {
                int pgid = dsProcessclient.Where(e => e.Client == item.projectgroup).Select(exp => exp.ProcessID).FirstOrDefault();
                int count = CompleteProjectwiseSummary[0].ProductionParentResult.Where(e => e.Projectgroupid == pgid).Count();
                if (count == 0 || count < 2)
                {
                    Toaster.Add("No Records found for the selected Project group", MatToastType.Info, "Global Dashboard");
                }
            }

            return Task.CompletedTask;
        }
        private async Task ViewEmployeeDetails(ParentDashboardResult item)
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                EmployeeProductionDetails = Enumerable.Empty<EmployeewiseProductionDashboardResult>();
                EmployeeBacklogDetails = Enumerable.Empty<EmployeewiseBacklogDashboardResult>();
                EmployeeEfficiencyDetails = Enumerable.Empty<EmployeeWiseEfficiencyDashboardResult>();

                DisplayLoading(true, "Loading");
                if (item.WorkedBy == "Production Data")
                {
                    isProductionHide = false; isBacklogHide = true; isEfficiencyHide = true;
                    string url = "/GlobalDashboard/GetEmployeewiseProductionDashboard/EmployeeID/" + EmpID + "/ProjectGroupID/" + item.Projectgroupid;
                    EmployeeProductionDetails = await ApiHelper.GetUsingMsgPackAsync<EmployeewiseProductionDashboardResult[]>(url, Http, AuthState.Token);

                }
                else if (item.WorkedBy == "BackLog Data")
                {
                    isProductionHide = true; isBacklogHide = false; isEfficiencyHide = true;
                    string url = "/GlobalDashboard/GetEmployeewiseBacklogDashboard/EmployeeID/" + EmpID + "/ProjectGroupID/" + item.Projectgroupid;
                    EmployeeBacklogDetails = await ApiHelper.GetUsingMsgPackAsync<EmployeewiseBacklogDashboardResult[]>(url, Http, AuthState.Token);
                }
                else if (item.WorkedBy == "Efficiency Data")
                {
                    isProductionHide = true; isBacklogHide = true; isEfficiencyHide = false;
                    string url = "/GlobalDashboard/GetEmployeeWiseEfficiencyDashboard/EmployeeID/" + EmpID + "/ProjectGroupID/" + item.Projectgroupid;
                    EmployeeEfficiencyDetails = await ApiHelper.GetUsingMsgPackAsync<EmployeeWiseEfficiencyDashboardResult[]>(url, Http, AuthState.Token);
                }
                modal.Open();
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error in view employee details", MatToastType.Danger, "Global Dahsboard");
                DisplayLoading(false);
            }
        }

        private Task OnCloseButtonClicked()
        {
            modal.Close();
            EmployeeProductionDetails = Enumerable.Empty<EmployeewiseProductionDashboardResult>();
            EmployeeBacklogDetails = Enumerable.Empty<EmployeewiseBacklogDashboardResult>();
            EmployeeEfficiencyDetails = Enumerable.Empty<EmployeeWiseEfficiencyDashboardResult>();
            return Task.CompletedTask;
        }
    }

}
