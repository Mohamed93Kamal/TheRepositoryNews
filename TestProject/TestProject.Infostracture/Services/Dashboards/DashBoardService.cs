using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Enums;
using TestProject.Core.ViewModels;
using TestProject.Web.Data;

namespace TestProject.Infostracture.Services.Dashboards
{
    public class DashBoardService : IDashBoardService
    {
        private readonly ApplicationDbContext _db;
        public DashBoardService (ApplicationDbContext db)
        {
            _db = db;

        }
        public async Task<DashBoardViewModel> GetData()
        {
            var data = new DashBoardViewModel();
            data.NumberOfUsers = await _db.Users.CountAsync(x => !x.IsDelete);
            data.NumberOfPost = await _db.posts.CountAsync(x => !x.IsDelete);
            data.NumberOfTrack = await _db.tracks.CountAsync(x => !x.IsDelete);
            data.NumberOfAdvertisement = await _db.Advertisements.CountAsync(x => !x.IsDelete);
            return data;
        }
        public async Task<List<PieChartViewModel>> GetUserTypeChart()
        {
            var data = new List<PieChartViewModel>();
            data.Add(new PieChartViewModel()
            {
                Key = "Administrator",
                Value =await _db.Users.CountAsync(x => !x.IsDelete && x.UserType == UserType.Administrator),
                Color = "Green"
            });
            data.Add(new PieChartViewModel()
            {
                Key = "Article Author",
                Value = await _db.Users.CountAsync(x => !x.IsDelete && x.UserType == UserType.ArticleAuthor),
                Color = "blue"
            });
            data.Add(new PieChartViewModel()
            {
                Key = "Track Administrator",
                Value = await _db.Users.CountAsync(x => !x.IsDelete && x.UserType == UserType.TrackAdministrator),
                Color = "Red"
            });
            data.Add(new PieChartViewModel()
            {
                Key = "Advertisement Owner",
                Value = await _db.Users.CountAsync(x => !x.IsDelete && x.UserType == UserType.AdvertisementOwner),
                Color = "Orange"
            });
            return data;
        }
    }
}
