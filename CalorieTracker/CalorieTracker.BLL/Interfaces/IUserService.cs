using CalorieTracker.DAL.DTO.Request;
using CalorieTracker.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.BLL.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<UserProfileResponse>> GetProfileAsync(string userId);
        Task<BaseResponse<UserProfileResponse>> UpdateProfileAsync(string userId, UpdateUserProfileRequest request);
        Task<BaseResponse> UpdateCalorieGoalAsync(string userId, UpdateCalorieGoalRequest request);
    }
}