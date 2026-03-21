using CalorieTracker.DAL.DTO.Request;
using CalorieTracker.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.BLL.Interfaces
{
    public interface IWeightLogService
    {
        Task<BaseResponse<WeightLogResponse>> AddWeightLogAsync(string userId, AddWeightLogRequest request);
        Task<BaseResponse<List<WeightLogResponse>>> GetAllLogsAsync(string userId);
        Task<BaseResponse<WeightProgressResponse>> GetProgressAsync(string userId);
        Task<BaseResponse> DeleteWeightLogAsync(string userId, int id);
    }
}
