using CalorieTracker.BLL.Interfaces;
using CalorieTracker.DAL.DTO.Request;
using CalorieTracker.DAL.DTO.Response;
using CalorieTracker.DAL.Models;
using CalorieTracker.DAL.Repositories.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.BLL.Service
{
    public class WeightLogService : IWeightLogService
    {
        private readonly IWeightLogRepository _weightLogRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public WeightLogService(
            IWeightLogRepository weightLogRepository,
            UserManager<ApplicationUser> userManager)
        {
            _weightLogRepository = weightLogRepository;
            _userManager = userManager;
        }

        public async Task<BaseResponse<WeightLogResponse>> AddWeightLogAsync(string userId, AddWeightLogRequest request)
        {
            try
            {
                var existingLog = await _weightLogRepository.Query()
                    .FirstOrDefaultAsync(w => w.UserId == userId &&
                                              w.LoggedAt.Date == request.LoggedAt.Date);

                if (existingLog is not null)
                    return new BaseResponse<WeightLogResponse>
                    {
                        Success = false,
                        Message = $"You already logged your weight on {request.LoggedAt:yyyy-MM-dd}"
                    };

                var weightLog = new WeightLog
                {
                    UserId = userId,
                    WeightInKg = request.WeightInKg,
                    LoggedAt = request.LoggedAt,
                    Notes = request.Notes
                };

                var saved = await _weightLogRepository.AddAsync(weightLog);

                //update CurrentWeight in the Profile
                var user = await _userManager.FindByIdAsync(userId);
                if (user is not null)
                {
                    user.CurrentWeight = request.WeightInKg;
                    await _userManager.UpdateAsync(user);
                }

                return new BaseResponse<WeightLogResponse>
                {
                    Success = true,
                    Message = "Weight logged successfully",
                    Data = saved.Adapt<WeightLogResponse>()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<WeightLogResponse>
                {
                    Success = false,
                    Message = "Failed to log weight",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
        public async Task<BaseResponse<List<WeightLogResponse>>> GetAllLogsAsync(string userId)
        {
            try
            {
                var logs = await _weightLogRepository.Query()
                    .Where(w => w.UserId == userId)
                    .OrderByDescending(w => w.LoggedAt)
                    .ToListAsync();

                if (!logs.Any())
                    return new BaseResponse<List<WeightLogResponse>>
                    {
                        Success = false,
                        Message = "No weight logs found"
                    };

                return new BaseResponse<List<WeightLogResponse>>
                {
                    Success = true,
                    Message = "Weight logs retrieved successfully",
                    Data = logs.Adapt<List<WeightLogResponse>>()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<WeightLogResponse>>
                {
                    Success = false,
                    Message = "Failed to get weight logs",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse<WeightProgressResponse>> GetProgressAsync(string userId)
        {
            try
            {
                var logs = await _weightLogRepository.Query()
                    .Where(w => w.UserId == userId)
                    .OrderBy(w => w.LoggedAt)
                    .ToListAsync();

                if (!logs.Any())
                    return new BaseResponse<WeightProgressResponse>
                    {
                        Success = false,
                        Message = "No weight logs found"
                    };

                var firstWeight = logs.First().WeightInKg;
                var lastWeight = logs.Last().WeightInKg;
                var totalChange = lastWeight - firstWeight;

                return new BaseResponse<WeightProgressResponse>
                {
                    Success = true,
                    Message = "Weight progress retrieved successfully",
                    Data = new WeightProgressResponse
                    {
                        FirstWeight = firstWeight,
                        LastWeight = lastWeight,
                        TotalChange = Math.Abs(totalChange),
                        ChangeDirection = totalChange < 0 ? "Lost" :
                                          totalChange > 0 ? "Gained" : "No Change",
                        Logs = logs.Adapt<List<WeightLogResponse>>()
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<WeightProgressResponse>
                {
                    Success = false,
                    Message = "Failed to get weight progress",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse> DeleteWeightLogAsync(string userId, int id)
        {
            try
            {
                var log = await _weightLogRepository.Query()
                    .FirstOrDefaultAsync(w => w.Id == id && w.UserId == userId);

                if (log is null)
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Weight log not found"
                    };

                await _weightLogRepository.DeleteAsync(id);
                return new BaseResponse
                {
                    Success = true,
                    Message = "Weight log deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Failed to delete weight log",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}