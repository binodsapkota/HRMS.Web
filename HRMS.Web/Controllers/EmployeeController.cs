using HRMS.Application.Interfaces;
using HRMS.Application.Services;
using HRMS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMS.Web.Controllers
{
    [Authorize(Roles ="HR")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _service;
        private readonly IDepartmentService _departmentService;
        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _service = employeeService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var data = await _departmentService.GetAllAsync();
            var departments = new SelectList(data.Data, "Id", "Name");
            ViewBag.Departments = departments;
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.AddAsync(employee);
                switch (result.Status)
                {
                    case Application.DTOs.ResultStatus.Success:
                        TempData["Message"] = result.Message;
                        return RedirectToAction("Index");
                    case Application.DTOs.ResultStatus.Failure:
                    default:
                        ViewBag.Message = result.Message;
                        break;
                }
            }

            await CreateDepartmentsViewbag();
            return View(employee);
        }

        private async Task CreateDepartmentsViewbag()
        {
            var data = await _departmentService.GetAllAsync();
            var departments = new SelectList(data.Data, "Id", "Name");
            ViewBag.Departments = departments;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _service.GetByIdAsync(id);
            switch (result.Status)
            {
                case Application.DTOs.ResultStatus.Failure:
                    TempData["Message"] = result.Message;
                    return RedirectToAction("Index");
                default:
                    break;
            }
            await CreateDepartmentsViewbag();
            return View(result.Data);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.UpdateAsync(employee);
                switch (result.Status)
                {
                    case Application.DTOs.ResultStatus.Success:
                        TempData["Message"] = result.Message;
                        return RedirectToAction("Index");
                    case Application.DTOs.ResultStatus.Failure:
                    default:
                        ViewBag.Message = result.Message;
                        break;
                } 
            }
            await CreateDepartmentsViewbag();
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.GetByIdAsync(id);
            switch (result.Status)
            {
                case Application.DTOs.ResultStatus.Failure:
                    TempData["Message"] = result.Message;
                    return RedirectToAction("Index");
                default:
                    break;
            }
            return View(result.Data);
        }
        [HttpPost("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _service.DeleteAsync(id);
            TempData["Message"] = result.Message;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _service.GetByIdAsync(id);
            switch (result.Status)
            {
                case Application.DTOs.ResultStatus.Failure:
                    TempData["Message"] = result.Message;
                    return RedirectToAction("Index");
                default:
                    break;
            }
            return View(result.Data);
        }
    }
}
