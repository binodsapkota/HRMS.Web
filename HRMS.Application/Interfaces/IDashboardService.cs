using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<int> GetTotalEmployee();
        Task<int> GetTotalLeaveRequest();
        Task<Dictionary<string, int>> GetEmployeeCountPerDepartment();
    }
}
