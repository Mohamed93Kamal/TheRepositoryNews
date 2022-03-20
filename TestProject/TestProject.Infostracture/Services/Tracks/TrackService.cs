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

namespace TestProject.Infostracture.Services.Tracks
{
    public class TrackService : ITrackService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;

        public TrackService(IEmailService emailService, ApplicationDbContext db, IMapper mapper, IUserService userService, IFileService fileService)
        {
            _emailService = emailService;
            _db = db;
            _mapper = mapper;
            _userService = userService;
            _fileService = fileService;

        }


        public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryString = _db.tracks.Include(x => x.PublishedBy).Include(x => x.Category).Where(x => !x.IsDelete).AsQueryable();
            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var Tracks = _mapper.Map<List<TrackViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new ResponseDto
            {
                data = Tracks,
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

        public async Task<int> Create(CreateTrackDto dto)
        {

            var track = _mapper.Map<Track>(dto);
            if (dto.TrackUrl != null)
            {
                track.TrackUrl = await _fileService.SaveFile(dto.TrackUrl, "Tracks");
            }
            await _db.tracks.AddAsync(track);
            await _db.SaveChangesAsync();
            return track.Id;
        }

        public async Task<int> Update(UpdateTrackDto dto)
        {


            var track = await _db.tracks.SingleOrDefaultAsync(x => x.Id == dto.Id && !x.IsDelete);
            if (track == null)
            {
                throw new EntityNotFoundException();
            }
            var UpdateTrack = _mapper.Map(dto, track);
            if (dto.TrackUrl != null)
            {
                track.TrackUrl = await _fileService.SaveFile(dto.TrackUrl, "Tracks");
            }

            _db.tracks.Update(UpdateTrack);
            await _db.SaveChangesAsync();

            return track.Id;
        }
        public async Task<List<ContentChangeLogViewModel>> GetLog(int id)
        {
            var changes = await _db.contentChangeLogs.Where(x => x.ContentId == id && x.Type == ContentType.Track).ToListAsync();
            return _mapper.Map<List<ContentChangeLogViewModel>>(changes);
        }

        public async Task<int> UpdateStatus(int id, ContentStatus status)
        {
            var track = await _db.tracks.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (track == null)
            {
                throw new EntityNotFoundException();
            }
            var changeLog = new ContentChangeLog();
            changeLog.ContentId = track.Id;
            changeLog.Type = ContentType.Track;
            changeLog.Old = track.Status;
            changeLog.New = status;
            changeLog.ChaneAt = DateTime.Now;

            await _db.contentChangeLogs.AddAsync(changeLog);
            await _db.SaveChangesAsync();

            track.Status = status;
            _db.tracks.Update(track);
            await _db.SaveChangesAsync();

            //await _emailService.Send(track.PublishedBy.Email, "UPDATE Track STATUS !", $"YOUR Track NOW IS {status.ToString()}");

            return track.Id;
        }
        public async Task<UpdateTrackDto> Get(int id)
        {
            var Track = await _db.tracks.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (Track == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<UpdateTrackDto>(Track);
        }

        public async Task<int> Delete(int id)
        {
            var Track = await _db.tracks.SingleOrDefaultAsync(x => x.Id == id && !x.IsDelete);
            if (Track == null)
            {
                throw new EntityNotFoundException();
            }
            Track.IsDelete = true;
            _db.tracks.Update(Track);
            await _db.SaveChangesAsync();
            return Track.Id;

        }

     

       
    }
}
