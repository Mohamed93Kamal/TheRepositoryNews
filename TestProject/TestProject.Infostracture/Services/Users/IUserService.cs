using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Dtos;
using TestProject.Core.ViewModels;

namespace TestProject.Infostracture.Services.Users
{
    public interface IUserService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<string> Create(CreateUserDto dto);
        Task<string> SetFCMToUser(string userId, string fcmToken);
        Task<string> Update(UpdateUserDto dto);
        Task<string> Delete(string Id);
        Task<byte[]> ExportToExcel();
        UserViewModel GetUsername(string username);
        Task<UpdateUserDto> Get(string Id);
        Task<List<UserViewModel>> GetAuthorList();
    }
}
