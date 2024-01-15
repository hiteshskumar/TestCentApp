namespace ChargesWFM.UI.Policies
{
    public class ProductionUploadAccessRequirement : IAccessRequirement
    {
        public ProductionUploadAccessRequirement()
        {
        }

        public string Module => "Production Upload";
    }
    public class ManualAssignAccessRequirement : IAccessRequirement
    {
        public ManualAssignAccessRequirement()
        {
        }

        public string Module => "Manual Assign";
    }
    public class UploadAccessRequirement : IAccessRequirement
    {
        public UploadAccessRequirement()
        {
        }

        public string Module => "Upload";
    }

    public class WorkableFilterAccessRequirement : IAccessRequirement
    {
        public WorkableFilterAccessRequirement()
        {
        }

        public string Module => "WorkableFilterUpload";
    }
    public class EditProductionAccessRequirement : IAccessRequirement
    {
        public EditProductionAccessRequirement()
        {
        }

        public string Module => "Edit Production";
    }
    public class InventoryDashboardAccessRequirement : IAccessRequirement
    {
        public InventoryDashboardAccessRequirement()
        {
        }
        public string Module => "GlobalDashboard";
    }
    public class ProductivityDashboardAccessRequirement : IAccessRequirement
    {
        public ProductivityDashboardAccessRequirement()
        {
        }
        public string Module => "Productivity DashBoard";
    }
    public class WorkAllocationAccessRequirement : IAccessRequirement
    {
        public WorkAllocationAccessRequirement()
        {
        }
        public string Module => "Work Allocation";
    }
    public class GlobalDashboardAccessRequirement : IAccessRequirement
    {
        public GlobalDashboardAccessRequirement()
        {
        }
        public string Module => "GlobalDashboard";

    }
    public class EmployeeCategoryMappingAccessRequirement : IAccessRequirement
    {
        public EmployeeCategoryMappingAccessRequirement()
        {
        }
        public string Module => "Employee Category Mapping";
    }
    public class ProductivityReportAccessRequirement : IAccessRequirement
    {
        public ProductivityReportAccessRequirement()
        {
        }
        public string Module => "Productivity DashBoard";
    }
}