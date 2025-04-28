
using HRMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Persistence.Repositories
{
    public class LeaveRequestRepository
    {
        private readonly HRMSDbContext _context;
        public LeaveRequestRepository(HRMSDbContext hRMSDbContext)
        {
            _context = hRMSDbContext;
        }
        public async Task<bool> AddAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Add(leaveRequest);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var emp = await _context.LeaveRequests.FindAsync(id);
            if (emp != null)
            {
                _context.LeaveRequests.Remove(emp);
                await _context.SaveChangesAsync();
                return true;
               
            }
            return false;

        }

        public async Task<IEnumerable<LeaveRequest>> GetAllAsync()
        {
            var data = await _context.LeaveRequests.Include(x=>x.User).ToListAsync();
            return data;
        }
        public async Task<IEnumerable<LeaveRequest>> GetAllByUserAsync(int userId)
        {
            var data = await _context.LeaveRequests.Include(x => x.User).Where(x=>x.UserId==userId).ToListAsync();
            return data;
        }
        public async Task<LeaveRequest> GetByIdAsync(int id)
        {
            var data = await _context.LeaveRequests.Include(x=>x.User).FirstOrDefaultAsync(e => e.Id == id);
            return data;
        }

        public async Task<bool> UpdateAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Update(leaveRequest);
            await _context.SaveChangesAsync();
            return true;
        }
       
    }
}
