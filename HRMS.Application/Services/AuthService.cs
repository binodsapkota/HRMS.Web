using HRMS.Application.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthRepository _authRepo;
        public AuthService(AuthRepository authRepository)
        {
            _authRepo = authRepository;
        }
        public Task<List<string>> GetUserRolesAsync(string email)
        {
            return _authRepo.GetUserRolesAsync(email);
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await _authRepo.GetUserByEmailAsync(email);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
                return null;

            return user;
        }

        public async Task<bool> RegisterAsync(string email, string password, string roleName)
        {
            if (await _authRepo.UserExistsAsync(email))
            {
                return false;
            }
            var role = await _authRepo.GetRoleByNameAsync(roleName);
            if (role == null)
            {
                throw new Exception("Role not found");
            }

            var user = new User()
            {
                Username = email,
                PasswordHash = HashPassword(password),
            };
            await _authRepo.AddUserAsync(user);

            var userRole = new UserRole()
            {
                UserId = user.Id,
                RoleId = role.Id

            };
            await _authRepo.AddUserRoleAsync(userRole);

            await _authRepo.SaveChangesAsync();
            return true;
        }

        public string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        private bool VerifyPassword(string password, string storedHash)
        {

            return HashPassword(password) == storedHash;
        }
    }
}