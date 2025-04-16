using HRMS.Application.Interfaces;
using HRMS.Application.Services;
using HRMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _service;
        public DepartmentController(IDepartmentService departmentService)
        {
            _service = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            var result = await _service.AddAsync(department);
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
            return View();
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
            return View(result.Data);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department department)
        {
            var result = await _service.UpdateAsync(department);
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
            return View();
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
