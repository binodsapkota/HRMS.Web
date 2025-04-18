using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Persistence.Repositories
{
    public class AuthRepository
    {
        private readonly HRMSDbContext _context;
        public AuthRepository(HRMSDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Username == email);
        }
        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Username == email);
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
        public async Task AddUserRoleAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
        }
        public async Task<List<string>> GetUserRolesAsync(string email)
        {
          var user=await GetUserByEmailAsync(email);
            if (user != null)
            {
                var roles = _context.UserRoles
                    .Include(x=>x.Role)
                    .Where(x => x.UserId == user.Id);

                return roles.Select(x=>x.Role.Name).ToList();

            }

            return new List<string>();
        }

        public async Task<int> SaveChangesAsync()
        {
          return  await _context.SaveChangesAsync();
        }
    }
}
