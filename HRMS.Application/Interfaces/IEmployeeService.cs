using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.DTOs;
namespace HRMS.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<ServiceResult<IEnumerable<Employee>>> GetAllAsync();
        Task<ServiceResult<Employee>> GetByIdAsync(Guid id);
        Task<ServiceResult<bool>> AddAsync(Employee employee);
        Task<ServiceResult<bool>> UpdateAsync(Employee employee);
        Task<ServiceResult<bool>> DeleteAsync(Guid id);
    }
}
