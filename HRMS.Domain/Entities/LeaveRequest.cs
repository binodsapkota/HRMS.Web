using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public User? User { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Reason { get; set; }

        public LeaveStatus Status { get; set; } //Pending,Approved, Rejected

        public DateTime RequestedDate { get; set; } = DateTime.Now;//setting default value to current date and time.
    }

    public enum LeaveStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
