using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Dtos;
using TestProject.Core.Enums;
using TestProject.Core.ViewModels;

namespace TestProject.Infostracture.Services.Tracks
{
    public interface ITrackService
    {
        Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<int> Create(CreateTrackDto dto);
        Task<int> UpdateStatus(int id, ContentStatus status);

        Task<List<ContentChangeLogViewModel>> GetLog(int id);
        Task<int> Update(UpdateTrackDto dto);
        Task<UpdateTrackDto> Get(int id);

        Task<int> Delete(int id);

    }
}
