using HRMS.Application.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMS.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRoleController : Controller
    {
        private readonly IUserService _userService;

        private readonly IUserRoleService _userRoleService;
        public UserRoleController(IUserService userService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _userRoleService.GetAllAsync();

            return View(data.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PrepareViewBag();
            return View();
        }

        private async Task PrepareViewBag()
        {
            var users = (await _userRoleService.GetAllUsersAsync()).Data;
            ViewBag.Users = new SelectList(users, "Id", "Username", "");

            var roles = (await _userRoleService.GetAllRolesAsync()).Data;
            ViewBag.Roles = new SelectList(roles, "Id", "Name", "");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserRole model)
        {
            if (ModelState.IsValid)
            {



                var result = await _userRoleService.AddAsync(model);
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

            await PrepareViewBag();
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _userRoleService.GetByIdAsync(id);
            switch (result.Status)
            {
                case Application.DTOs.ResultStatus.Failure:
                    TempData["Message"] = result.Message;
                    return RedirectToAction("Index");
                default:
                    break;
            }
            await PrepareViewBag();
            return View(result.Data);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserRole model)
        {
            if (ModelState.IsValid)
            {
                

                var result = await _userRoleService.UpdateAsync(model);
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
            await PrepareViewBag();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userRoleService.GetByIdAsync(id);
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
            var result = await _userRoleService.DeleteAsync(id);
            TempData["Message"] = result.Message;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _userRoleService.GetByIdAsync(id);
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
