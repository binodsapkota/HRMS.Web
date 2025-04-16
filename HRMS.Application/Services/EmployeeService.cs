using HRMS.Application.DTOs;
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
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository _employeeRepo;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepo = employeeRepository;
        }

        public async Task<ServiceResult<bool>> AddAsync(Employee employee)
        {
            var result = await _employeeRepo.AddAsync(employee);
            if (result)
                return new ServiceResult<bool>
                {
                    Message = "Employee Added Successfully",
                    Status = ResultStatus.Success,
                    Data = true
                };
            else
                return new ServiceResult<bool>
                {
                    Message = "Employee Added Successfully",
                    Status = ResultStatus.Failure,
                    Data = false
                };
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var result = await _employeeRepo.DeleteAsync(id);
            if (result)
                return new ServiceResult<bool>
                {
                    Data = true,
                    Status = ResultStatus.Failure,
                    Message = "Deleted"
                };
            else
                return new ServiceResult<bool>
                {
                    Data = false,
                    Status = ResultStatus.Failure,
                    Message = "Can't Delete"
                };

        }

        public async Task<ServiceResult<IEnumerable<Employee>>> GetAllAsync()
        {
            var data = await _employeeRepo.GetAllAsync();
            return new ServiceResult<IEnumerable<Employee>>
            {
                Data = data
            };
        }

        public async Task<ServiceResult<Employee>> GetByIdAsync(int id)
        {
            var data = await _employeeRepo.GetByIdAsync(id);
            return new ServiceResult<Employee>
            {
                Data = data
            };
        }

        public async Task<ServiceResult<bool>> UpdateAsync(Employee employee)
        {
            var result = await _employeeRepo.UpdateAsync(employee);
            if (result)
                return new ServiceResult<bool>
                {
                    Message = "Employee Updated Successfully",
                    Status = ResultStatus.Success,
                    Data = true
                };
            else
                return new ServiceResult<bool>
                {
                    Message = "Employee Updated Successfully",
                    Status = ResultStatus.Failure,
                    Data = false
                };
        }
    }
}
