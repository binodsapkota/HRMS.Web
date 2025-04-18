using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(string email, string password, string roleName);
        Task<User> LoginAsync(string email, string password);
        Task<List<string>> GetUserRolesAsync(string email);
        string HashPassword(string password);
    }
}
