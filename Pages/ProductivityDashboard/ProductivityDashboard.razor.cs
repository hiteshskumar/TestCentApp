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

namespace ChargesWFM.UI.Pages.ProductivityDashboard
{
    public partial class ProductivityDashboard
    {
        [Inject]
        HttpClient? Http { get; set; }
        [Inject]
        AuthenticationStateProvider? AuthProvider { get; set; }
        [Inject]
        ILocalStorageService? LocalStorageService { get; set; }
        [Inject]
        IToasterService? ToasterService { get; set; }
        [Inject]
        IMatToaster? Toaster { get; set; }
        [Inject]
        IBlazorDownloadFileService? BlazorDownloadFileService { get; set; }
        private Tuple<string, GridSortOrder>? sortColumn;
        private int EmpID;
        public int PGroupID = 0;
        public string? schemaName;
        private bool displayLoading;
        private string? progressText { get; set; }
        private GetBillableGroupResults[]? dsBillableGroup;
        private GetReporteeLocationMappingResults[]? dsLocation;
        private GetReportingAuthorityResults[]? dsReportingAuthority;
        private GetInventoryMastersResults[]? dsInventoryMaster;
        private GetMasterDetails[]? dsCD;
        private IEnumerable<GetHourlyProductivityDashboardResult> _hourlyDashboard = Enumerable.Empty<GetHourlyProductivityDashboardResult>();
        private IEnumerable<GetHourlyProductivityDashboardWithSubTaskResult> _subTaskHourlyDashboard = Enumerable.Empty<GetHourlyProductivityDashboardWithSubTaskResult>();
        private IEnumerable<GetHourlyAccountCountDashboardResult> _HourlyAccountCountDashboard = Enumerable.Empty<GetHourlyAccountCountDashboardResult>();
        private IEnumerable<GetHourlyEfficiencyDashboardResult> _EfficiencyDashboard = Enumerable.Empty<GetHourlyEfficiencyDashboardResult>();
        private List<string> selectedBillableGroup { get; set; }
        private List<string> selectedCD { get; set; }
        private List<string> selectedLocation { get; set; }
        private List<string> selectedReportingAuthority { get; set; }

        private string BillableGroup, Location, ReportingAuthority = "EMPTY", ClientDispositionCode = "EMPTY";
        private string selectedLevel = "";
        private int tabIndex = 0;
        private bool isHideClientDisposition = true;
        private bool isHide = true;
        private bool isAccountCountHide = true;
        protected override async Task OnInitializedAsync()
        {
            var user = await LocalStorageService.GetAsync<AuthenticatedUser>();
            EmpID = user.EmployeeId;
            var projectGroup = await LocalStorageService.GetAsync<NexgenProjectGroups>();
            PGroupID = projectGroup.ProjectGroupID;
            schemaName = projectGroup.SchemaName;
            await GetBillableGroup(PGroupID);
            await GetLocation(PGroupID);
            await GetReportingAuthority();
            isHide = true;
            if (PGroupID == 6632 || PGroupID == 6633)
            {
                isHideClientDisposition = false;
                await GetClientDisposition(PGroupID);
            }
            else
            {
                isHideClientDisposition = true;
            }
            if (PGroupID == 6641 || PGroupID == 6640)
            {
                isAccountCountHide = false;
            }
            else
            {
                isAccountCountHide = true;
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
            await GetBillableGroup(PGroupID);
            await GetLocation(PGroupID);
            await GetReportingAuthority();
            isHide = true;
            tabIndex = 0;
            ReportingAuthority = "EMPTY";
            ClientDispositionCode = "EMPTY";
            if (PGroupID == 6632 || PGroupID == 6633)
            {
                isHideClientDisposition = false;
                await GetClientDisposition(PGroupID);
            }
            else
            {
                isHideClientDisposition = true;
            }
            if (PGroupID == 6640 || PGroupID == 6641)
            {
                isAccountCountHide = false;
            }
            else
            {
                isAccountCountHide = true;
            }
            this.StateHasChanged();
        }
        public async Task GetBillableGroup(int PGroupID)
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                string BillableGroupURL = "/ProductivityDashboard/GetBillableGroups/ProjectGroupID/" + PGroupID;
                dsBillableGroup = await ApiHelper.GetUsingMsgPackAsync<GetBillableGroupResults[]>(BillableGroupURL, Http, AuthState.Token);
                selectedBillableGroup = dsBillableGroup.Select(x => x.BillableGroupId.ToString()).ToList();
                BillableGroup = string.Join(',', selectedBillableGroup).ToString();
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error occured while loading Billable Group", MatToastType.Danger, "Productivity Dashboard");
                DisplayLoading(false);
            }
            finally
            {
                StateHasChanged();
            }
        }


        public async Task GetLocation(int PGroupID)
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                string LocationURL = "/ProductivityDashboard/GetReporteeLocationMappings";
                dsLocation = await ApiHelper.GetUsingMsgPackAsync<GetReporteeLocationMappingResults[]>(LocationURL, Http, AuthState.Token);
                selectedLocation = dsLocation.Select(x => x.Location.ToString()).ToList();
                Location = string.Join(',', selectedLocation).ToString();
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error occured while loading Billable Group", MatToastType.Danger, "Productivity Dashboard");
                DisplayLoading(false);
            }
            finally
            {
                StateHasChanged();
            }
        }

        public async Task GetReportingAuthority()
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                string ReportingAuthorityURL = "/ProductivityDashboard/GetReportingAuthority/EmployeeID/" + EmpID;
                dsReportingAuthority = await ApiHelper.GetUsingMsgPackAsync<GetReportingAuthorityResults[]>(ReportingAuthorityURL, Http, AuthState.Token);
                selectedReportingAuthority = dsReportingAuthority.Select(x => x.ReportingAuthorityID.ToString()).ToList();
                if (dsReportingAuthority.Count() == 0)
                {
                    ReportingAuthority = "EMPTY";
                }
                else
                {
                    ReportingAuthority = string.Join(',', selectedReportingAuthority).ToString();
                }
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error occured while loading Billable Group", MatToastType.Danger, "Productivity Dashboard");
                DisplayLoading(false);
            }
            finally
            {
                StateHasChanged();
            }
        }

        public async Task GetClientDisposition(int PGroupID)
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                string ClientDispositionCodeURL = "InventoryDashboard/GetInventoryMasters/EmployeeID/" + EmpID + "/ProjectGroupID/" + PGroupID;
                dsInventoryMaster = await ApiHelper.GetUsingMsgPackAsync<GetInventoryMastersResults[]>(ClientDispositionCodeURL, Http, AuthState.Token);
                dsCD = dsInventoryMaster[0].ClientDisposition.ToArray();
                selectedCD = dsCD.Select(x => x.MasterValue.ToString()).ToList();
                ClientDispositionCode = string.Join(',', selectedCD).ToString();
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error occured while loading Billable Group", MatToastType.Danger, "Productivity Dashboard");
                DisplayLoading(false);

            }
            finally
            {
                StateHasChanged();
            }
        }
        protected async Task OnGetValueClicked()
        {
            await OnActiveIndexChanged(tabIndex);
        }
        public async Task GetHourlyDashboardAsync()
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                var HourlyDashboardInput = new GetHourlyDashboardInput()
                {
                    Schema = schemaName,
                    EmployeeID = EmpID,
                    ProjectgroupID = PGroupID,
                    BillableGroup = BillableGroup,
                    Location = Location,
                    ReportingAuthority = ReportingAuthority,
                    ClientDisposition = ClientDispositionCode
                };
                // string strurl = "/ProductivityDashboard/FetchHourlyProductivityDashboard/Schema" + schemaName + "/EmployeeID/" + EmpID + "/ProjectgroupID/" + PGroupID +
                // "/BillableGroup/" + BillableGroup + "/Location/" + Location + "/ReportingAuthority/" + ReportingAuthority + "/ClientDisposition/" + ClientDispositionCode;
                _hourlyDashboard = await ApiHelper.PostUsingMsgPackAsync<GetHourlyProductivityDashboardResult[]>
                ("/ProductivityDashboard/FetchHourlyProductivityDashboard", HourlyDashboardInput, Http, AuthState.Token);

                if (_hourlyDashboard.Count() == 0)
                {
                    isHide = true;
                    Toaster.Add("No Accounts for the selected Project", MatToastType.Danger, "Productivity Dashboard");
                }
                else
                {
                    isHide = false;
                }
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                isHide = true;
                Toaster.Add("Error occured while loading Productivity", MatToastType.Danger, "Productivity Dashboard");
                DisplayLoading(false);
            }
            finally
            {
                StateHasChanged();
            }
        }


        public async Task GetSubTaskDashboardAsync()
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                var HourlyDashboardwithsubtaskInput = new GetHourlyDashboardwithsubtaskInput()
                {
                    Schema = schemaName,
                    EmployeeID = EmpID,
                    ProjectgroupID = PGroupID,
                    BillableGroup = BillableGroup
                };
                // string strurl = "/ProductivityDashboard/FetchHourlyProductivityDashboardWithSubTask/Schema/" + schemaName + "/EmployeeID/" + EmpID + "/ProjectgroupID/" + PGroupID +
                // "/BillableGroup/" + BillableGroup;

                _subTaskHourlyDashboard = await ApiHelper.PostUsingMsgPackAsync<GetHourlyProductivityDashboardWithSubTaskResult[]>("/ProductivityDashboard/FetchHourlyProductivityDashboardWithSubTask", HourlyDashboardwithsubtaskInput, Http, AuthState.Token);
                if (_subTaskHourlyDashboard.Count() == 0)
                {
                    Toaster.Add("No Accounts for the select project group", MatToastType.Danger, "Productivity Dashboard");
                }
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error occured while loading Productivity", MatToastType.Danger, "Productivity Dashboard");
                DisplayLoading(false);
            }
            finally
            {
                StateHasChanged();
            }
        }

        public async Task GetEfficiencyDashboardAsync()
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                var HourlyDashboardInput = new GetHourlyDashboardInput()
                {
                    Schema = schemaName,
                    EmployeeID = EmpID,
                    ProjectgroupID = PGroupID,
                    BillableGroup = BillableGroup,
                    Location = Location,
                    ReportingAuthority = ReportingAuthority,
                    ClientDisposition = ClientDispositionCode
                };
                // string strurl = "/ProductivityDashboard/FetchHourlyEfficiencyDashboard/Schema/" + schemaName + "/EmployeeID/" + EmpID + "/ProjectgroupID/" + PGroupID +
                // "/BillableGroup/" + BillableGroup + "/Location/" + Location + "/ReportingAuthority/" + ReportingAuthority + "/ClientDisposition/" + ClientDispositionCode;

                _EfficiencyDashboard = await ApiHelper.PostUsingMsgPackAsync<GetHourlyEfficiencyDashboardResult[]>("/ProductivityDashboard/FetchHourlyEfficiencyDashboard", HourlyDashboardInput, Http, AuthState.Token);
                if (_EfficiencyDashboard.Count() == 0)
                {
                    Toaster.Add("No Accounts for the select project group", MatToastType.Danger, "Productivity Dashboard");
                }
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error occured while loading Productivity", MatToastType.Danger, "Productivity Dashboard");
                DisplayLoading(false);
            }
            finally
            {
                StateHasChanged();
            }
        }
        public async Task GetAccountCountDashboardAsync()
        {
            try
            {
                DisplayLoading(true, "Loading");
                var AuthState = (AuthStateProvider)AuthProvider;
                var HourlyDashboardInput = new GetHourlyDashboardInput()
                {
                    Schema = schemaName,
                    EmployeeID = EmpID,
                    ProjectgroupID = PGroupID,
                    BillableGroup = BillableGroup,
                    Location = Location,
                    ReportingAuthority = ReportingAuthority,
                    ClientDisposition = ClientDispositionCode
                };

                _HourlyAccountCountDashboard = await ApiHelper.PostUsingMsgPackAsync<GetHourlyAccountCountDashboardResult[]>("/ProductivityDashboard/FetchHourlyAccountCountDashboard", HourlyDashboardInput, Http, AuthState.Token);
                if (_HourlyAccountCountDashboard.Count() == 0)
                {
                    Toaster.Add("No Accounts for the select project group", MatToastType.Danger, "Productivity Dashboard");
                }
                DisplayLoading(false);
            }
            catch (Exception ex)
            {
                Toaster.Add("Error occured while loading Productivity", MatToastType.Danger, "Productivity Dashboard");
                DisplayLoading(false);
            }
            finally
            {
                StateHasChanged();
            }
        }

        protected async Task OnActiveIndexChanged(int Index)
        {
            
            tabIndex = Index;
            _hourlyDashboard = Enumerable.Empty<GetHourlyProductivityDashboardResult>();
            _subTaskHourlyDashboard = Enumerable.Empty<GetHourlyProductivityDashboardWithSubTaskResult>();
            // _clientDispositionHourlyDashboard = Enumerable.Empty<HourlyDashboardModel>();;
            if (!selectedBillableGroup.Any())
            {
                Toaster.Add("Please select Billable Group", MatToastType.Warning, "Hourly Dashboard");
                return;
            }
            else if (!selectedLocation.Any())
            {
                Toaster.Add("Please select location", MatToastType.Warning, "Hourly Dashboard");
                return;
            }
            if (tabIndex == 0)
            {
                Location = string.Join(',', selectedLocation).ToString();
                BillableGroup = string.Join(',', selectedBillableGroup).ToString();
                if (selectedReportingAuthority.Count == 0)
                {
                    ReportingAuthority = "EMPTY";
                }
                else
                {
                    ReportingAuthority = string.Join(',', selectedReportingAuthority).ToString();
                }
                await GetHourlyDashboardAsync();
            }
            else if (tabIndex == 1)
            {
                BillableGroup = string.Join(',', selectedBillableGroup).ToString();
                await GetSubTaskDashboardAsync();
            }
            else if (tabIndex == 2)
            {
                Location = string.Join(',', selectedLocation).ToString();
                BillableGroup = string.Join(',', selectedBillableGroup).ToString();
                if (selectedReportingAuthority.Count == 0)
                {
                    ReportingAuthority = "EMPTY";
                }
                else
                {
                    ReportingAuthority = string.Join(',', selectedReportingAuthority).ToString();
                }
                await GetEfficiencyDashboardAsync();
            }
            else if (tabIndex == 3 && (PGroupID == 6641 || PGroupID == 6640))
            {
                Location = string.Join(',', selectedLocation).ToString();
                BillableGroup = string.Join(',', selectedBillableGroup).ToString();
                _HourlyAccountCountDashboard = Enumerable.Empty<GetHourlyAccountCountDashboardResult>();
                if (selectedReportingAuthority.Count == 0)
                {
                    ReportingAuthority = "EMPTY";
                }
                else
                {
                    ReportingAuthority = string.Join(',', selectedReportingAuthority).ToString();
                }
                await GetAccountCountDashboardAsync();
            }
        }

    }

}

