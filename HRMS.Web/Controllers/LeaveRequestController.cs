using HRMS.Application.DTOs;
using HRMS.Application.Interfaces;
using HRMS.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRMS.Web.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private int userId { get { return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); } }
        public LeaveRequestController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
            
        }

        // GET: LeaveRequestController
        
        public async Task<ActionResult> Index()
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);

            var leaveRequests = await _leaveRequestService.GetByUserAsync(userId);
            return View(leaveRequests.Data);
        }
        [Authorize(Roles ="Admin,HR")]
        public async Task<ActionResult> Manage()
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);

            var leaveRequests = await _leaveRequestService.GetAllAsync();
            return View(leaveRequests.Data);
        }


        // GET: LeaveRequestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveRequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveRequestDto model)
        {
            try
            {
                var result = await _leaveRequestService.CreateAsync(model, userId);

                switch (result.Status)
                {

                    case ResultStatus.Success:
                        return RedirectToAction("Index");
                       
                    case ResultStatus.Failure:

                    default:
                        break;
                }

                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

       
    }
}
