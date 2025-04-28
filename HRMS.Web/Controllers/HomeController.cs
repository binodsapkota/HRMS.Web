using System.Diagnostics;
using HRMS.Application.Interfaces;
using HRMS.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private IDashboardService _dashboardService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IDashboardService dashboardService)
        {
            _logger = logger;
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            DashboardViewModel model = new DashboardViewModel()
            {
                TotalEmployee = await _dashboardService.GetTotalEmployee(),
                TotalLeaveRequest = await _dashboardService.GetTotalLeaveRequest(),
                DepartmentEmployeeCount=await _dashboardService.GetEmployeeCountPerDepartment()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
