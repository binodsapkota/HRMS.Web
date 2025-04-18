using HRMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.DTOs
{
    /// <summary>
    /// Service result class is a generic class,
    /// 
    /// where t is dynamic object
    /// </summary>
    /// <typeparam name="T">dynamic</typeparam>
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
