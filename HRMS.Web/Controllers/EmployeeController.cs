using ClosedXML.Excel;
using HRMS.Application.Interfaces;
using HRMS.Application.Services;
using HRMS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;

namespace HRMS.Web.Controllers
{
    [Authorize(Roles = "HR")]
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
        public async Task<IActionResult> ExportToExcel()
        {
            var data = await _service.GetAllAsync();

            using (var workBook = new XLWorkbook())
            {
                var workSheet = workBook.Worksheets.Add("Employees");
                workSheet.Cell(1, 1).Value = "ID";
                workSheet.Cell(1, 2).Value = "Name";
                workSheet.Cell(1, 3).Value = "Email";
                workSheet.Cell(1, 4).Value = "Department";

                int row = 2;

                foreach (var employee in data.Data)
                {
                    workSheet.Cell(row, 1).Value = employee.Id;
                    workSheet.Cell(row, 2).Value = employee.Fullname;
                    workSheet.Cell(row, 3).Value = employee.Email;
                    workSheet.Cell(row, 4).Value = employee.Department?.Name;

                    row++;
                }

                using (var stream= new MemoryStream())
                {
                    workBook.SaveAs(stream);

                    stream.Position = 0;

                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Employee.xlsx"
                        );
                }
            }

           
        }

        [HttpGet]
        public IActionResult ExportToPdf()
        {
            var data =  _service.GetAllAsync().Result;
            return new ViewAsPdf("ExportToPdf", data.Data) {FileName="Employee.pdf" };
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
