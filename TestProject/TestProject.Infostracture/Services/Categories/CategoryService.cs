using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Dtos;
using TestProject.Core.Exceptions;
using TestProject.Core.ViewModels;
using TestProject.Data.Models;
using TestProject.Web.Data;

namespace TestProject.Infostracture.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public CategoryService(ApplicationDbContext db , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<CategoryViewModel>> GetCategoryList()
        {
            var categories = await _db.categories.Where(x => !x.IsDelete ).ToListAsync();
            return _mapper.Map<List<CategoryViewModel>>(categories);
        }

        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.categories.Where(x => !x.IsDelete && (x.Name.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch))).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var categories = _mapper.Map<List<CategoryViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = categories,
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


        public async Task<int> Create(CreateCategoryDto dto)
        {
            var DuplicateName = _db.categories.Any(x => !x.IsDelete && (x.Name == dto.Name));
            if (DuplicateName)
            {
                throw new DuplicateNameCategoryException();
            }

            var category = _mapper.Map<Category>(dto);         
            await _db.categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return category.Id;
        }

        public async Task<int> Update(UpdateCategoryDto dto)
        {
           
            var category = await _db.categories.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if(category == null)
            {
                throw new EntityNotFoundException();
            }
            var UpdateCategory = _mapper.Map<UpdateCategoryDto , Category>(dto, category);
            _db.categories.Update(UpdateCategory);
            await _db.SaveChangesAsync();
            return UpdateCategory.Id;
        }


        public async Task<int> Delete(int Id)
        {
            var category = await _db.categories.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (category == null)
            {
                throw new EntityNotFoundException();
            }

            category.IsDelete = true;
            _db.categories.Update(category);
            await _db.SaveChangesAsync();
            return category.Id;
        }


        public async Task<UpdateCategoryDto> Get(int id)
        {
            var user = await _db.categories.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateCategoryDto>(user);
        }

    }
}
