using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Core.Constants;
using TestProject.Core.Dtos;
using TestProject.Infostracture.Services.Users;

namespace TestProject.Web.Controllers
{
    public class UserController : BaseController
    {
      
        public UserController(IUserService userService) : base(userService)
        {
          
        }


        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            return File(await _userService.ExportToExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
        }

        [HttpGet]
        public async Task<IActionResult> AddFCM(string fcm)
        {
            await _userService.SetFCMToUser(userId, fcm);
            return Ok("Updated FCM User");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        public async Task<JsonResult> GetDataUser(Pagination pagination, Query query)
        {
            var result = await _userService.GetAll(pagination, query);
            return Json(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateUserDto dto)
        {
            if (ModelState.IsValid)
            {
                await _userService.Create(dto);
                return Ok(Result.AddSuccessResult());
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user = await _userService.Get(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UpdateUserDto dto)
        {
            if (ModelState.IsValid)
            {
                await _userService.Update(dto);
                return Ok(Result.EditSuccessResult());

            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.Delete(id);
            return Ok(Result.DeleteSuccessResult());

        }
    }
}
