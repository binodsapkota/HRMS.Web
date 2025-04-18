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
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepo;

        public UserService(UserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        public async Task<ServiceResult<bool>> AddAsync(User user)
        {
            var result = await _userRepo.AddAsync(user);
            if (result)
                return new ServiceResult<bool>
                {
                    Message = "User Added Successfully",
                    Status = ResultStatus.Success,
                    Data = true
                };
            else
                return new ServiceResult<bool>
                {
                    Message = "User Added Successfully",
                    Status = ResultStatus.Failure,
                    Data = false
                };
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var result = await _userRepo.DeleteAsync(id);
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

        public async Task<ServiceResult<IEnumerable<User>>> GetAllAsync()
        {
            var data = await _userRepo.GetAllAsync();
            return new ServiceResult<IEnumerable<User>>
            {
                Data = data
            };
        }

        public async Task<ServiceResult<User>> GetByIdAsync(int id)
        {
            var data = await _userRepo.GetByIdAsync(id);
            return new ServiceResult<User>
            {
                Data = data,
                Status=ResultStatus.Success
            };
        }

        public async Task<ServiceResult<bool>> UpdateAsync(User user)
        {
            var result = await _userRepo.UpdateAsync(user);
            if (result)
                return new ServiceResult<bool>
                {
                    Message = "User Updated Successfully",
                    Status = ResultStatus.Success,
                    Data = true
                };
            else
                return new ServiceResult<bool>
                {
                    Message = "User Updated Successfully",
                    Status = ResultStatus.Failure,
                    Data = false
                };
        }
    }
}
