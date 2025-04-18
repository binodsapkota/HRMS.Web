using HRMS.Application.Interfaces;
using HRMS.Application.Services;
using HRMS.Domain.Entities;
using HRMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMS.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        private readonly IAuthService _authService;
        public UserController(IUserService userService, IAuthService authService)
        {
            _service = userService;
            _authService = authService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            var registerUsers = (from user in data.Data
                                 select new RegisterViewModel()
                                 {
                                     Username = user.Username,
                                     Email = user.Username
                                 }

                               ).ToList();
            return View(registerUsers);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new User()
                {
                    Username = model.Username,
                    PasswordHash = _authService.HashPassword(model.Password)
                };

                var result = await _service.AddAsync(user);
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


            return View(model);
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
        public async Task<IActionResult> Edit(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user= _service.GetByIdAsync(model.Id).Result.Data;
                user.PasswordHash = _authService.HashPassword(model.Password);
                var result = await _service.UpdateAsync(user);
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

            return View(model);
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
