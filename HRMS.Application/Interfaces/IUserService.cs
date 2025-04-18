using HRMS.Application.DTOs;
using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<IEnumerable<User>>> GetAllAsync();
        Task<ServiceResult<User>> GetByIdAsync(int id);
        Task<ServiceResult<bool>> AddAsync(User user);
        Task<ServiceResult<bool>> UpdateAsync(User User);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}
