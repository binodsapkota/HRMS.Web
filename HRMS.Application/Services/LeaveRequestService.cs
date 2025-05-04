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
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly LeaveRequestRepository _repository;

        public LeaveRequestService(LeaveRequestRepository leaveRequestRepository)
        {
            _repository = leaveRequestRepository;
        }

        public async Task<ServiceResult<bool>> CreateAsync(CreateLeaveRequestDto dto, int userId)
        {
            var leaveRequest = new LeaveRequest()
            {
                EndDate = dto.EndDate,
                UserId = userId,
                StartDate = dto.StartDate,
                Reason = dto.Reason,
                Status = LeaveStatus.Pending,
                RequestedDate = DateTime.Now

            };

            await _repository.AddAsync(leaveRequest);
            return new ServiceResult<bool>
            {
                Data = true,
                Status = ResultStatus.Success,
            };
        }

        public async Task<ServiceResult<List<LeaveRequestDto>>> GetAllAsync()
        {
            var data = (from c in (await _repository.GetAllAsync())
                        select new LeaveRequestDto
                        {
                            UserId = c.UserId,
                            UserName = c.User.Username,
                            StartDate = c.StartDate,
                            EndDate = c.EndDate,
                            Reason = c.Reason,
                            Id = c.Id,
                            LeaveStatus = c.Status,
                            RequestDate = c.RequestedDate
                        }
                      ).ToList();

            return new ServiceResult<List<LeaveRequestDto>>()
            {
                Data = data,
                Status = ResultStatus.Success,
            };
        }

        public async Task<ServiceResult<LeaveRequestDto>> GetByIdAsync(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data != null)
            {
                return new ServiceResult<LeaveRequestDto>()
                {
                    Data = new LeaveRequestDto
                    {
                        UserId = data.UserId,
                        UserName = data.User.Username,
                        StartDate = data.StartDate,
                        EndDate = data.EndDate,
                        Reason = data.Reason,
                        Id = data.Id,
                        LeaveStatus = data.Status,
                        RequestDate = data.RequestedDate
                    },
                    Status = ResultStatus.Success,

                };
            }
            return new ServiceResult<LeaveRequestDto>()
            {
                Data = null,
                Status = ResultStatus.Failure,
                Message = "No Leave Request Found"
            };
        }

        public async Task<ServiceResult<List<LeaveRequestDto>>> GetByUserAsync(int userid)
        {
            var data = (from c in (await _repository.GetAllByUserAsync(userid))
                        select new LeaveRequestDto
                        {
                            UserId = c.UserId,
                            UserName = c.User.Username,
                            StartDate = c.StartDate,
                            EndDate = c.EndDate,
                            Reason = c.Reason,
                            Id = c.Id,
                            LeaveStatus = c.Status,
                            RequestDate = c.RequestedDate
                        }
                      ).ToList();

            return new ServiceResult<List<LeaveRequestDto>>()
            {
                Data = data,
                Status = ResultStatus.Success,
            };
        }

        public async Task<ServiceResult<bool>> UpdateAsync(int leaveRequestId, LeaveStatus status)
        {
            var model = await _repository.GetByIdAsync(leaveRequestId);
            if (model != null)
            {
                model.Status = status;
                await _repository.UpdateAsync(model);
                return new ServiceResult<bool>()
                {
                    Data = true,
                    Status = ResultStatus.Success,
                    Message = "Leave Status Updated"
                };
            }

            return new ServiceResult<bool>()
            {
                Data = false,
                Status = ResultStatus.Failure,
                Message = "No Leave Request Found"
            };
        }
    }
}
