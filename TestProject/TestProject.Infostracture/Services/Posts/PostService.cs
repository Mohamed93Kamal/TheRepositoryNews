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
using TestProject.Web.Data;

namespace TestProject.Infostracture.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IFileService _fileService;
       
        public PostService( IEmailService emailService, IFileService fileService, ApplicationDbContext db, IMapper mapper)
        {
            _db = db;          
            _mapper = mapper;
            _emailService = emailService;
            _fileService = fileService;
        }

        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.posts
                .Include(x => x.Attatchments)
                .Include(x => x.category)
                .Include(x => x.Author)
                .Where(x => !x.IsDelete).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var posts = _mapper.Map<List<PostviewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = posts,
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

        public async Task<int> Create(CreatePostDto dto)
        {
            var post = _mapper.Map<Post>(dto);

            await _db.posts.AddAsync(post);
            await _db.SaveChangesAsync();

            if (dto.Attachments != null)
            {
                foreach (var a in dto.Attachments)
                {
                    var postAttachment = new PostAttatchment();
                    postAttachment.AttachmentUrl = await _fileService.SaveFile(a, "Images");
                    postAttachment.PostId = post.Id;
                    await _db.postAttatchments.AddAsync(postAttachment);
                    await _db.SaveChangesAsync();
                }
            }

            return post.Id;
        }


        public async Task<int> Update(UpdatePostDto dto)
        {
            var post = await _db.posts.SingleOrDefaultAsync(x => x.Id == dto.Id && !x.IsDelete);
            if (post == null)
            {
                throw new EntityNotFoundException();
            }

            var updatedPost = _mapper.Map(dto, post);


            _db.posts.Update(updatedPost);
            await _db.SaveChangesAsync();

            if (dto.Attachments != null)
            {
                foreach (var a in dto.Attachments)
                {
                    var postAttachment = new PostAttatchment();
                    postAttachment.AttachmentUrl = await _fileService.SaveFile(a, "Images");
                    postAttachment.PostId = post.Id;
                    await _db.postAttatchments.AddAsync(postAttachment);
                    await _db.SaveChangesAsync();
                }
            }

            return post.Id;
        }


        public async Task<UpdatePostDto> Get(int id)
        {
            var post = await _db.posts.Include(x => x.Attatchments).SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (post == null)
            {
                throw new EntityNotFoundException();
            }

            var dto = _mapper.Map<UpdatePostDto>(post);

            if (post.Attatchments != null)
            {
                dto.PostAttachments = _mapper.Map<List<PostAttachmentViewModel>>(post.Attatchments);
            }

            return dto;
        }

        public async Task<int> RemoveAttachment(int id)
        {
            var post = await _db.postAttatchments.SingleOrDefaultAsync(x => x.Id == id);
            if (post == null)
            {
                throw new EntityNotFoundException();
            }
            _db.postAttatchments.Remove(post);
            await _db.SaveChangesAsync();
            return post.Id;
        }

        public async Task<int> UpdateStatus(int id, ContentStatus status)
        {
            var post = await _db.posts.Include(x => x.Author).SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);

            if (post == null)
            {
                throw new EntityNotFoundException();
            }

            var changeLog = new ContentChangeLog();
            changeLog.ContentId = post.Id;
            changeLog.Type = ContentType.Post;
            changeLog.Old = post.Status;
            changeLog.New = status;
            changeLog.ChaneAt = DateTime.Now;

            await _db.contentChangeLogs.AddAsync(changeLog);
            await _db.SaveChangesAsync();

            post.Status = status;
            _db.posts.Update(post);
            await _db.SaveChangesAsync();
          await  _emailService.Send(post.Author.Email, "UPDATE POST STATUS !", $"YOUR POST NOW IS {status.ToString()}");

            return post.Id;
        }

        public async Task<List<ContentChangeLogViewModel>> GetLog(int id)
        {
            var changes =await _db.contentChangeLogs.Where(x => x.ContentId == id && x.Type == ContentType.Post).ToListAsync();
            return _mapper.Map<List<ContentChangeLogViewModel>>(changes);
        }

        public async Task<int> Delete(int id)
        {
            var post = await _db.posts.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (post == null)
            {
                throw new EntityNotFoundException();
            }
            post.IsDelete = true;
            _db.posts.Update(post);
            await _db.SaveChangesAsync();   
            return post.Id;
        }

       

    }
}
