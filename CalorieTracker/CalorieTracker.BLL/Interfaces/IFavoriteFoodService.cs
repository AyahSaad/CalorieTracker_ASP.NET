using CalorieTracker.DAL.DTO.Request;
using CalorieTracker.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.BLL.Interfaces
{
    public interface IFavoriteFoodService
    {
        Task<BaseResponse<FavoriteFoodResponse>> AddFavoriteAsync(string userId, AddFavoriteFoodRequest request);
        Task<BaseResponse<List<FavoriteFoodResponse>>> GetAllFavoritesAsync(string userId);
        Task<BaseResponse> DeleteFavoriteAsync(string userId, int id);
        Task<BaseResponse<MealFoodResponse>> AddFavoriteToMealAsync(string userId, int favoriteId, int mealId, AddFoodToMealRequest request);
    }
}
