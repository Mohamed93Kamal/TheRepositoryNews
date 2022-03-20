using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Core.Constants;
using TestProject.Core.Dtos;
using TestProject.Infostracture.Services.Advertisements;
using TestProject.Infostracture.Services.Categories;
using TestProject.Infostracture.Services.Users;

namespace TestProject.Web.Controllers
{
    public class AdvertisementController : BaseController
    {
      
        private readonly IAdvertisementService _advertisementService;
        public AdvertisementController(IAdvertisementService advertisementService, IUserService userService) : base(userService)
        {
            _advertisementService = advertisementService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        public async Task<JsonResult> GetDataAdvertisement(Pagination pagination, Query query)
        {
            var result = await _advertisementService.GetAll(pagination, query);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Owners"] = new SelectList(await _advertisementService.GetAdvertisementOwners() , "Id" , "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvertisementDto dto)
        {
            if(!string.IsNullOrWhiteSpace(dto.OwnerId))
            {
                ModelState.Remove("Owner.FullName");
                ModelState.Remove("Owner.Email");
                ModelState.Remove("Owner.PhoneNumber");
            }
           

            if (ModelState.IsValid)
            {
                await _advertisementService.Create(dto);
                return Ok(Result.AddSuccessResult());
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _advertisementService.Get(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAdvertisementDto dto)
        {
            if (ModelState.IsValid)
            {
                await _advertisementService.Update(dto);
                return Ok(Result.EditSuccessResult());

            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _advertisementService.Delete(id);
            return Ok(Result.DeleteSuccessResult());

        }
    }
}
