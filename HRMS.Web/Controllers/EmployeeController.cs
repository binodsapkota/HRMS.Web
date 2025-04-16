using HRMS.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeDto model)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(EmployeeDto model)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Details(Guid Id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(Guid Id)
        {
            return View();
        }
        [HttpPost("Delete")]
        public IActionResult DeleteConfirmed(Guid Id)
        {
            return View();
        }
    }
}
