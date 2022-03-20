using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Infostracture.Services.Dashboards;
using TestProject.Infostracture.Services.Users;

namespace TestProject.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IDashBoardService _dashBoardService;
        public HomeController (IDashBoardService dashBoardService, IUserService userService) : base(userService)
        {
            _dashBoardService = dashBoardService;
        }
        public async Task<IActionResult> Index()
        {
            if (userType != "Administrator")
            {
                return Redirect("/category");
            }
            var data = await _dashBoardService.GetData();
            return View(data);
        }

        public async Task<IActionResult> GetUserTypeChartData()
        {
            var data = await _dashBoardService.GetUserTypeChart();
            return Ok(data);
        }

    }
}
