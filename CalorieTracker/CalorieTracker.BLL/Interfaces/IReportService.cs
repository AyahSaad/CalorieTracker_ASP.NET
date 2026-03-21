using CalorieTracker.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.BLL.Interfaces
{
    public interface IReportService
    {
        Task<BaseResponse<DailyReportResponse>> GetDailyReportAsync(string userId, DateTime date);
        Task<BaseResponse<WeeklyReportResponse>> GetWeeklyReportAsync(string userId, DateTime startDate);
    
   }
}
