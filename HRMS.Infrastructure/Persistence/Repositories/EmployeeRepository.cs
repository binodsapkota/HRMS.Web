
using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository 
    {
        private readonly HRMSDbContext _context;
        public EmployeeRepository(HRMSDbContext hRMSDbContext)
        {
            _context = hRMSDbContext;
        }
        public async Task<bool> AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                await _context.SaveChangesAsync();
                return true;
               
            }
            return false;

        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var data = await _context.Employees.Include(e => e.Department).ToListAsync();
            return data;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var data = await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);
            return data;
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
