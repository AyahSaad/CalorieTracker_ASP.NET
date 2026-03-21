using CalorieTracker.DAL.DTO.Request;
using CalorieTracker.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.BLL.Interfaces
{
    public interface IMealService
    {
        Task<BaseResponse<MealResponse>> CreateMealAsync(string userId, AddMealRequest request);
        Task<BaseResponse<MealFoodResponse>> AddFoodToMealAsync(string userId, int mealId, AddFoodToMealRequest request);
        Task<BaseResponse<List<MealResponse>>> GetMealsByDateAsync(string userId, DateTime date);
        Task<BaseResponse<MealResponse>> GetMealByIdAsync(string userId, int mealId);
        Task<BaseResponse> DeleteMealAsync(string userId, int mealId);
        Task<BaseResponse> RemoveFoodFromMealAsync(string userId, int mealFoodId);
        Task<BaseResponse<MealResponse>> UpdateMealAsync(string userId, int mealId, UpdateMealRequest request);
        Task<BaseResponse> DeleteAllMealsByDateAsync(string userId, DateTime date);
        Task<BaseResponse<List<MealResponse>>> GetAllMealsAsync(string userId);
    }
}
