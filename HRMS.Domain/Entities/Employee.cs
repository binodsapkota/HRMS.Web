using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; } = string.Empty;//it will set default value as string.empty

        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        //designation
        //department
        public Guid DepartmentId { get; set; }

        public Department? Department { get; set; }

    }
}
