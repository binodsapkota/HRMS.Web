using HRMS.Application.DTOs;
using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Interfaces
{
    public interface ILeaveRequestService
    {
        Task<ServiceResult<List<LeaveRequestDto>>> GetAllAsync();
        Task<ServiceResult<List<LeaveRequestDto>>> GetByUserAsync(int userid);

        Task<ServiceResult<LeaveRequestDto>> GetByIdAsync(int id);
        Task<ServiceResult<bool>> CreateAsync(CreateLeaveRequestDto dto, int userId);
        Task<ServiceResult<bool>> UpdateAsync(int leaveRequestId, LeaveStatus status);
    }
}
