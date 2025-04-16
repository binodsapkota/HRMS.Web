using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs
{
    public class ServiceResult<T>
    {
        public T Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public ResultStatus Status { get; set; }
    }


    public enum ResultStatus
    {
        Failure,
        Success,
    }

}
