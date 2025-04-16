using HRMS.Application.DTOs;
using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<ServiceResult<IEnumerable<Department>>> GetAllAsync();
        Task<ServiceResult<Department>> GetByIdAsync(int id);
        Task<ServiceResult<bool>> AddAsync(Department department);
        Task<ServiceResult<bool>> UpdateAsync(Department department);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}
