using Newtonsoft.Json.Linq;

namespace HRMS.Web.Models
{
    public class DashboardViewModel
    {
        public int TotalEmployee { get; set; }
        public int TotalLeaveRequest { get; set; }
        public int TotalApprovedLeaves { get; set; }
        public int TotalPendingLeaves { get; set; }


        public Dictionary<string,int> DepartmentEmployeeCount { get; set; }


    }
}
