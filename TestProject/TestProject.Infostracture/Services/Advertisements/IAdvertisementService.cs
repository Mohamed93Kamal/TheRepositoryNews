using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Dtos;
using TestProject.Core.ViewModels;

namespace TestProject.Infostracture.Services.Advertisements
{
   public interface IAdvertisementService
    {
        Task<int> Delete(int id);
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<int> Create(CreateAdvertisementDto dto);
        Task<List<UserViewModel>> GetAdvertisementOwners();
        Task<int> Update(UpdateAdvertisementDto dto);
        Task<UpdateAdvertisementDto> Get(int id);
    }
}
