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
    public class EmployeeRepository : IEmployeeService
    {
        private readonly HRMSDbContext _context;
        public EmployeeRepository(HRMSDbContext hRMSDbContext)
        {
            _context = hRMSDbContext;
        }
        public async Task<ServiceResult<bool>> AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return new ServiceResult<bool>
            {
                Message = "Employee Added Successfully",
                Status = ResultStatus.Success,
                Data = true
            };
        }

        public async Task<ServiceResult<bool>> DeleteAsync(Guid id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
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

        public async Task<ServiceResult<IEnumerable<Employee>>> GetAllAsync()
        {
            var data = await _context.Employees.Include(e => e.Department).ToListAsync();
            return new ServiceResult<IEnumerable<Employee>>
            {
                Data = data
            };
        }

        public async Task<ServiceResult<Employee>> GetByIdAsync(Guid id)
        {
            var data = await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);
            return new ServiceResult<Employee>
            {
                Data = data
            };
        }

        public async Task<ServiceResult<bool>> UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return new ServiceResult<bool>
            {
                Message = "Employee Updated Successfully",
                Status = ResultStatus.Success,
                Data = true
            };
        }
    }
}
