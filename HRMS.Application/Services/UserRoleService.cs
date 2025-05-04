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
    public class UserRoleService : IUserRoleService
    {
        private readonly UserRoleRepository _userRoleRepo;
        private readonly UserRepository _userRepo;


        public UserRoleService(UserRoleRepository userRoleRepository, UserRepository userRepository)
        {
            _userRoleRepo = userRoleRepository;
            _userRepo = userRepository;
        }

        public async Task<ServiceResult<bool>> AddAsync(UserRole userRole)
        {
            var result = await _userRoleRepo.AddAsync(userRole);
            if (result)
                return new ServiceResult<bool>
                {
                    Message = "UserRole Added Successfully",
                    Status = ResultStatus.Success,
                    Data = true
                };
            else
                return new ServiceResult<bool>
                {
                    Message = "UserRole Added Successfully",
                    Status = ResultStatus.Failure,
                    Data = false
                };
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var result = await _userRoleRepo.DeleteAsync(id);
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

        public async Task<ServiceResult<IEnumerable<UserRole>>> GetAllAsync()
        {
            var data = await _userRoleRepo.GetAllAsync();
            return new ServiceResult<IEnumerable<UserRole>>
            {
                Data = data
            };
        }

        public async Task<ServiceResult<List<Role>>> GetAllRolesAsync()
        {
            var data = await _userRepo.GetRoleAsync();
            return new ServiceResult<List<Role>>()
            {
                Data = data,
                Status = ResultStatus.Success,


            };
        }

        public async Task<ServiceResult<List<User>>> GetAllUsersAsync()
        {
            var data = await _userRepo.GetAllAsync();
            return new ServiceResult<List<User>>()
            {
                Data = data.ToList(),
                Status = ResultStatus.Success
            };
        }

        public async Task<ServiceResult<UserRole>> GetByIdAsync(int id)
        {
            var data = await _userRoleRepo.GetByIdAsync(id);
            return new ServiceResult<UserRole>
            {
                Data = data,
                Status = ResultStatus.Success
            };
        }

        public async Task<ServiceResult<bool>> UpdateAsync(UserRole userRole)
        {
            var result = await _userRoleRepo.UpdateAsync(userRole);
            if (result)
                return new ServiceResult<bool>
                {
                    Message = "UserRole Updated Successfully",
                    Status = ResultStatus.Success,
                    Data = true
                };
            else
                return new ServiceResult<bool>
                {
                    Message = "UserRole Updated Successfully",
                    Status = ResultStatus.Failure,
                    Data = false
                };
        }


    }
}
