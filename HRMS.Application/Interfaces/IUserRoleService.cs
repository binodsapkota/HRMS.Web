using HRMS.Application.DTOs;
using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Interfaces
{
    public interface IUserRoleService
    {
        Task<ServiceResult<IEnumerable<UserRole>>> GetAllAsync();
        Task<ServiceResult<UserRole>> GetByIdAsync(int id);
        Task<ServiceResult<bool>> AddAsync(UserRole user);
        Task<ServiceResult<bool>> UpdateAsync(UserRole User);
        Task<ServiceResult<bool>> DeleteAsync(int id);
        Task<ServiceResult<List<User>>> GetAllUsersAsync();
        Task<ServiceResult<List<Role>>> GetAllRolesAsync();
    }
}
