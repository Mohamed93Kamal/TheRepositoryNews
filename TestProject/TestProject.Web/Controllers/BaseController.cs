using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Infostracture.Services.Users;

namespace TestProject.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly IUserService _userService;
        protected string userType;
        protected string userId;
        public BaseController(IUserService userService)
        {
            _userService = userService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if(User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = _userService.GetUsername(userName);
                userId = user.Id;
                userType = user.UserType;
                ViewBag.fullName = user.FullName;
                ViewBag.UserType = user.UserType.ToString();
                ViewBag.img = user.ImgUrl;

            }
        }
    }
}
