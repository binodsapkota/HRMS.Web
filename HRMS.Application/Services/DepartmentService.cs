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
    public class DepartmentService : IDepartmentService
    {
        private readonly DepartmentRepository _departmentRepo;

        public DepartmentService(DepartmentRepository departmentRepository)
        {
            _departmentRepo = departmentRepository;
        }

        public async Task<ServiceResult<bool>> AddAsync(Department department)
        {
            var result = await _departmentRepo.AddAsync(department);
            if (result)
                return new ServiceResult<bool>
                {
                    Message = "Department Added Successfully",
                    Status = ResultStatus.Success,
                    Data = true
                };
            else
                return new ServiceResult<bool>
                {
                    Message = "Department Added Successfully",
                    Status = ResultStatus.Failure,
                    Data = false
                };
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var result = await _departmentRepo.DeleteAsync(id);
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

        public async Task<ServiceResult<IEnumerable<Department>>> GetAllAsync()
        {
            var data = await _departmentRepo.GetAllAsync();
            return new ServiceResult<IEnumerable<Department>>
            {
                Data = data
            };
        }

        public async Task<ServiceResult<Department>> GetByIdAsync(int id)
        {
            var data = await _departmentRepo.GetByIdAsync(id);
            return new ServiceResult<Department>
            {
                Data = data,
                Status = data == null ? ResultStatus.Failure : ResultStatus.Success,
                Message="Department Not Found. Invalid Identifier."
            };
        }

        public async Task<ServiceResult<bool>> UpdateAsync(Department department)
        {
            var result = await _departmentRepo.UpdateAsync(department);
            if (result)
                return new ServiceResult<bool>
                {
                    Message = "Department Updated Successfully",
                    Status = ResultStatus.Success,
                    Data = true
                };
            else
                return new ServiceResult<bool>
                {
                    Message = "Department Updated Successfully",
                    Status = ResultStatus.Failure,
                    Data = false
                };
        }
    }
}
