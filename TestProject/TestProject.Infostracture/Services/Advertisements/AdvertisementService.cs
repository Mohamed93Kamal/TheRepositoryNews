using AutoMapper;
using CMC.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Dtos;
using TestProject.Core.Enums;
using TestProject.Core.Exceptions;
using TestProject.Core.ViewModels;
using TestProject.Data.Models;
using TestProject.Infostracture.Services.Users;
using TestProject.Web.Data;

namespace TestProject.Infostracture.Services.Advertisements
{
   public class AdvertisementService : IAdvertisementService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        public AdvertisementService(ApplicationDbContext db , IMapper mapper , IUserService userService , IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
            _userService = userService;
        }

        public async Task<List<UserViewModel>> GetAdvertisementOwners()
        {
            var users = await _db.Users.Where(x => !x.IsDelete && x.UserType == UserType.AdvertisementOwner).ToListAsync();
            return _mapper.Map<List<UserViewModel>>(users);
        }

        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.Advertisements.Include(x => x.Owner).Where(x => !x.IsDelete).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var advertisements = _mapper.Map<List<AdvertisementViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = advertisements,
                meta = new Meta
                {
                    page = pagination.Page,
                    perpage = pagination.PerPage,
                    pages = pages,
                    total = dataCount,
                }
            };
            return result;
        }

        public async Task<int> Create(CreateAdvertisementDto dto)
        {
            

            if(dto.StartDate >= dto.EndDate)
            {
                throw new InvalidDateException();
            }

           var advertisemnet = _mapper.Map<Advertisement>(dto);
           if(dto.ImgUrl != null)
            {
              advertisemnet.ImgUrl = await _fileService.SaveFile(dto.ImgUrl, "Images");
            }

           if(!string.IsNullOrWhiteSpace(dto.OwnerId))
            {
                advertisemnet.OwnerId = dto.OwnerId;
            }

          await  _db.Advertisements.AddAsync(advertisemnet);
          await  _db.SaveChangesAsync();

            if(advertisemnet.OwnerId == null)
            {
                var userId = await _userService.Create(dto.Owner);
                advertisemnet.OwnerId = userId;
                _db.Advertisements.Update(advertisemnet);
                await _db.SaveChangesAsync();
            }
           
            return advertisemnet.Id;
        }

        public async Task<int> Update(UpdateAdvertisementDto dto)
        {

            if (dto.StartDate >= dto.EndDate)
            {
                throw new InvalidDateException();
            }
            var advertisemnet = await _db.Advertisements.SingleOrDefaultAsync(x => x.Id == dto.Id && !x.IsDelete);
            if(advertisemnet == null)
            {
                throw new EntityNotFoundException();
            }
            var UpdateAdvertisemnet = _mapper.Map(dto , advertisemnet);
            if (dto.ImgUrl != null)
            {
                UpdateAdvertisemnet.ImgUrl = await _fileService.SaveFile(dto.ImgUrl, "Images");
            }
        
            _db.Advertisements.Update(UpdateAdvertisemnet);
            await _db.SaveChangesAsync();

            return advertisemnet.Id;
        }

        public async Task<int> Delete (int id)
        {
            var advertisement =await _db.Advertisements.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if(advertisement == null)
            {
                throw new EntityNotFoundException();
            }
            advertisement.IsDelete = true;
            _db.Advertisements.Update(advertisement);
            await _db.SaveChangesAsync();
            return advertisement.Id;
            
        }

        public async Task<UpdateAdvertisementDto> Get(int id)
        {
            var advertisement = await _db.Advertisements.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (advertisement == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateAdvertisementDto>(advertisement);
        }
    }
}
