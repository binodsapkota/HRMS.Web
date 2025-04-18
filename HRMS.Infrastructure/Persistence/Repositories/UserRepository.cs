
using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Persistence.Repositories
{
    public class UserRepository
    {
        private readonly HRMSDbContext _context;
        public UserRepository(HRMSDbContext hRMSDbContext)
        {
            _context = hRMSDbContext;
        }
        public async Task<bool> AddAsync(User User)
        {
            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var emp = await _context.Users.FindAsync(id);
            if (emp != null)
            {
                _context.Users.Remove(emp);
                await _context.SaveChangesAsync();
                return true;
               
            }
            return false;

        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var data = await _context.Users.ToListAsync();
            return data;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var data = await _context.Users.FirstOrDefaultAsync(e => e.Id == id);
            return data;
        }

        public async Task<bool> UpdateAsync(User User)
        {
            _context.Users.Update(User);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
