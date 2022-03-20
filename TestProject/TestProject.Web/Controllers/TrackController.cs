using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Core.Constants;
using TestProject.Core.Dtos;
using TestProject.Core.Enums;
using TestProject.Infostracture.Services.Advertisements;
using TestProject.Infostracture.Services.Categories;
using TestProject.Infostracture.Services.Tracks;
using TestProject.Infostracture.Services.Users;

namespace TestProject.Web.Controllers
{
    public class TrackController : BaseController
    {
      
        private readonly ITrackService _trackService;
        private readonly ICategoryService _categoryService;
        public TrackController(ITrackService trackService , ICategoryService categoryService, IUserService userService) : base(userService)
        {
            _trackService = trackService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        public async Task<JsonResult> GetDataTrack(Pagination pagination, Query query)
        {
            var result = await _trackService.GetAll(pagination, query);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["categories"] = new SelectList(await _categoryService.GetCategoryList() , "Id" , "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTrackDto dto)
        {
          
            if (ModelState.IsValid)
            {
                await _trackService.Create(dto);
                return Ok(Result.AddSuccessResult());
            }
            ViewData["categories"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "Name");

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var track = await _trackService.Get(id);
            ViewData["categories"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "Name");

            return View(track);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTrackDto dto)
        {
            if (ModelState.IsValid)
            {
                await _trackService.Update(dto);
                return Ok(Result.EditSuccessResult());

            }
            ViewData["categories"] = new SelectList(await _categoryService.GetCategoryList(), "Id", "Name");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetLog(int Id)
        {
            var logs = await _trackService.GetLog(Id);
            return View(logs);
        }



        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int id, ContentStatus status)
        {
            await _trackService.UpdateStatus(id, status);

            return Ok(Result.UpdateStatusSuccessResult());
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _trackService.Delete(id);
            return Ok(Result.DeleteSuccessResult());

        }
    }
}
