
using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Persistence.Repositories
{
    public class UserRoleRepository
    {
        private readonly HRMSDbContext _context;
        public UserRoleRepository(HRMSDbContext hRMSDbContext)
        {
            _context = hRMSDbContext;
        }
        public async Task<bool> AddAsync(UserRole UserRole)
        {
            _context.UserRoles.Add(UserRole);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var emp = await _context.UserRoles.FindAsync(id);
            if (emp != null)
            {
                _context.UserRoles.Remove(emp);
                await _context.SaveChangesAsync();
                return true;
               
            }
            return false;

        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            var data = await _context.UserRoles.Include(x => x.User).ToListAsync();
            return data;
        }

        public async Task<UserRole> GetByIdAsync(int id)
        {
            var data = await _context.UserRoles.Include(x => x.User).FirstOrDefaultAsync(e => e.Id == id);
            return data;
        }

        public async Task<bool> UpdateAsync(UserRole UserRole)
        {
            _context.UserRoles.Update(UserRole);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
