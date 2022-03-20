using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.ViewModels;

namespace TestProject.Infostracture.Services.Dashboards
{
    public interface IDashBoardService
    {
        Task<DashBoardViewModel> GetData();
        Task<List<PieChartViewModel>> GetUserTypeChart();
    }
}
