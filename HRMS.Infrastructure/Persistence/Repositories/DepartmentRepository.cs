using HRMS.Application.DTOs;
using HRMS.Application.Interfaces;
using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Persistence.Repositories
{
    public class DepartmentRepository : IDepartmentService
    {
        private readonly HRMSDbContext _context;
        public DepartmentRepository(HRMSDbContext hRMSDbContext)
        {
            _context = hRMSDbContext;
        }
        public async Task<ServiceResult<bool>> AddAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return new ServiceResult<bool>
            {
                Message = "Department Added Successfully",
                Status = ResultStatus.Success,
                Data = true
            };
        }

        public async Task<ServiceResult<bool>> DeleteAsync(Guid id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
                return new ServiceResult<bool>
                {
                    Data = true,
                    Status = ResultStatus.Success,
                    Message = "Deleted"
                };
            }
            return new ServiceResult<bool>
            {
                Data = false,
                Status = ResultStatus.Failure,
                Message = "Can't Delete"
            };

        }

        public async Task<ServiceResult<IEnumerable<Department>>> GetAllAsync()
        {
            var data = await _context.Departments.ToListAsync();
            return new ServiceResult<IEnumerable<Department>>
            {
                Data = data
            };
        }

        public async Task<ServiceResult<Department>> GetByIdAsync(Guid id)
        {
            var data = await _context.Departments.FirstOrDefaultAsync(e => e.Id == id);
            return new ServiceResult<Department>
            {
                Data = data
            };
        }

        public async Task<ServiceResult<bool>> UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
            return new ServiceResult<bool>
            {
                Message = "Department Updated Successfully",
                Status = ResultStatus.Success,
                Data = true
            };
        }
    }
}
