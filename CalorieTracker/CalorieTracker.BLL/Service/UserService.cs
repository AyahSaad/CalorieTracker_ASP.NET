using CalorieTracker.BLL.Interfaces;
using CalorieTracker.DAL.DTO.Request;
using CalorieTracker.DAL.DTO.Response;
using CalorieTracker.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace CalorieTracker.BLL.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<BaseResponse<UserProfileResponse>> GetProfileAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                    return new BaseResponse<UserProfileResponse>
                    {
                        Success = false,
                        Message = "User not found"
                    };

                return new BaseResponse<UserProfileResponse>
                {
                    Success = true,
                    Message = "Profile retrieved successfully",
                    Data = user.Adapt<UserProfileResponse>()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserProfileResponse>
                {
                    Success = false,
                    Message = "Failed to get profile",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse<UserProfileResponse>> UpdateProfileAsync(string userId, UpdateUserProfileRequest request)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                    return new BaseResponse<UserProfileResponse>
                    {
                        Success = false,
                        Message = "User not found"
                    };

                if (request.FullName is not null) user.FullName = request.FullName;
                if (request.City is not null) user.City = request.City;
                if (request.Street is not null) user.Street = request.Street;
                if (request.Height is not null) user.Height = request.Height;
                if (request.Age is not null) user.Age = request.Age;
                if (request.Gender is not null) user.Gender = request.Gender;

                await _userManager.UpdateAsync(user);

                return new BaseResponse<UserProfileResponse>
                {
                    Success = true,
                    Message = "Profile updated successfully",
                    Data = user.Adapt<UserProfileResponse>()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserProfileResponse>
                {
                    Success = false,
                    Message = "Failed to update profile",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse> UpdateCalorieGoalAsync(
            string userId, UpdateCalorieGoalRequest request)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "User not found"
                    };

                user.DailyCalorieGoal = request.DailyCalorieGoal;
                await _userManager.UpdateAsync(user);

                return new BaseResponse
                {
                    Success = true,
                    Message = $"Daily calorie goal updated to {request.DailyCalorieGoal}"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Failed to update calorie goal",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}