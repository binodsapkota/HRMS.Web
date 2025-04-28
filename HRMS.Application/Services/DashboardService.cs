using HRMS.Application.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly LeaveRequestRepository _leaveRequestRepo;
        private readonly EmployeeRepository _employeeRepo;
        public DashboardService(LeaveRequestRepository leaveRequestRepository, EmployeeRepository employeeRepository)
        {
            _employeeRepo = employeeRepository;
            _leaveRequestRepo = leaveRequestRepository;
        }

        public async Task<Dictionary<string, int>> GetEmployeeCountPerDepartment()
        {
            var data = (await _employeeRepo.GetAllAsync())
                 .GroupBy(x => x.Department.Name)
                 .ToDictionary(g => g.Key, g => g.Count())
                 ;
            return data;
        }

        public async Task<int> GetTotalEmployee()
        {
            return (await _employeeRepo.GetAllAsync()).Count();
        }

        public async Task<int> GetTotalLeaveRequest()
        {
            return (await _leaveRequestRepo.GetAllAsync()).Count();
        }
    }
}
