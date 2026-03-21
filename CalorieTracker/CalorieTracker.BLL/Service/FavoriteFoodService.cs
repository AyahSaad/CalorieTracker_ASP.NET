using CalorieTracker.BLL.Interfaces;
using CalorieTracker.DAL.DTO.Request;
using CalorieTracker.DAL.DTO.Response;
using CalorieTracker.DAL.Models;
using CalorieTracker.DAL.Repositories.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.BLL.Service
{
    public class FavoriteFoodService : IFavoriteFoodService
    {
        private readonly IFavoriteFoodRepository _favoriteFoodRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly IMealRepository _mealRepository;

        public FavoriteFoodService(
            IFavoriteFoodRepository favoriteFoodRepository,
            IFoodRepository foodRepository,
            IMealRepository mealRepository)
        {
            _favoriteFoodRepository = favoriteFoodRepository;
            _foodRepository = foodRepository;
            _mealRepository = mealRepository;
        }

        public async Task<BaseResponse<FavoriteFoodResponse>> AddFavoriteAsync(
            string userId, AddFavoriteFoodRequest request)
        {
            try
            {
                var food = await _foodRepository.GetByIdAsync(request.FoodId);
                if (food is null)
                    return new BaseResponse<FavoriteFoodResponse>
                    {
                        Success = false,
                        Message = "Food not found"
                    };

                var exists = await _favoriteFoodRepository
                    .ExistsAsync(userId, request.FoodId);
                if (exists)
                    return new BaseResponse<FavoriteFoodResponse>
                    {
                        Success = false,
                        Message = "Food already in favorites"
                    };

                var favorite = new FavoriteFood
                {
                    UserId = userId,
                    FoodId = request.FoodId
                };

                var saved = await _favoriteFoodRepository.AddAsync(favorite);
                saved.Food = food;

                return new BaseResponse<FavoriteFoodResponse>
                {
                    Success = true,
                    Message = "Food added to favorites successfully",
                    Data = new FavoriteFoodResponse
                    {
                        Id = saved.Id,
                        FoodId = food.Id,
                        FoodName = food.Name,
                        CaloriesPer100g = food.CaloriesPer100g,
                        ImageUrl = food.ImageUrl,
                        Measures = food.Measures.Adapt<List<FoodMeasureResponse>>()
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<FavoriteFoodResponse>
                {
                    Success = false,
                    Message = "Failed to add favorite",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse<List<FavoriteFoodResponse>>> GetAllFavoritesAsync(
            string userId)
        {
            try
            {
                var favorites = await _favoriteFoodRepository.Query()
                    .Where(f => f.UserId == userId)
                    .ToListAsync();

                if (!favorites.Any())
                    return new BaseResponse<List<FavoriteFoodResponse>>
                    {
                        Success = false,
                        Message = "No favorite foods found"
                    };

                return new BaseResponse<List<FavoriteFoodResponse>>
                {
                    Success = true,
                    Message = "Favorites retrieved successfully",
                    Data = favorites.Select(f => new FavoriteFoodResponse
                    {
                        Id = f.Id,
                        FoodId = f.Food.Id,
                        FoodName = f.Food.Name,
                        CaloriesPer100g = f.Food.CaloriesPer100g,
                        ImageUrl = f.Food.ImageUrl,
                        Measures = f.Food.Measures.Adapt<List<FoodMeasureResponse>>()
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<FavoriteFoodResponse>>
                {
                    Success = false,
                    Message = "Failed to get favorites",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse> DeleteFavoriteAsync(string userId, int id)
        {
            try
            {
                var favorite = await _favoriteFoodRepository.Query()
                    .FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);

                if (favorite is null)
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Favorite not found"
                    };

                await _favoriteFoodRepository.DeleteAsync(id);
                return new BaseResponse
                {
                    Success = true,
                    Message = "Removed from favorites successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Failed to remove favorite",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse<MealFoodResponse>> AddFavoriteToMealAsync(string userId, int favoriteId, int mealId, AddFoodToMealRequest request)
        {
            try
            {
                var favorite = await _favoriteFoodRepository.Query()
                    .FirstOrDefaultAsync(f => f.Id == favoriteId &&
                                              f.UserId == userId);
                if (favorite is null)
                    return new BaseResponse<MealFoodResponse>
                    {
                        Success = false,
                        Message = "Favorite not found"
                    };

                var meal = await _mealRepository.GetUserMealAsync(mealId, userId);
                if (meal is null)
                    return new BaseResponse<MealFoodResponse>
                    {
                        Success = false,
                        Message = "Meal not found"
                    };

                var food = favorite.Food;
                var measure = food.Measures
                    .FirstOrDefault(m => m.Id == request.MeasureId);
                if (measure is null)
                    return new BaseResponse<MealFoodResponse>
                    {
                        Success = false,
                        Message = "Measure not found"
                    };

                var totalWeight = measure.WeightInGrams * request.Quantity;
                var totalCalories = (food.CaloriesPer100g / 100) * totalWeight;
                var totalProtein = (food.ProteinPer100g  / 100) * totalWeight;
                var totalFat = (food.FatPer100g      / 100) * totalWeight;
                var totalCarbs = (food.CarbsPer100g    / 100) * totalWeight;

                var mealFood = new MealFood
                {
                    MealId             = mealId,
                    FoodId             = food.Id,
                    Quantity           = request.Quantity,
                    MeasureUnit        = measure.MeasureName,
                    TotalWeightInGrams = totalWeight,
                    TotalCalories      = totalCalories,
                    TotalProtein       = totalProtein,
                    TotalFat           = totalFat,
                    TotalCarbs         = totalCarbs
                };

                var saved = await _mealRepository.AddFoodToMealAsync(mealFood);
                saved.Food = food;

                return new BaseResponse<MealFoodResponse>
                {
                    Success = true,
                    Message = "Favorite food added to meal successfully",
                    Data = saved.Adapt<MealFoodResponse>()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<MealFoodResponse>
                {
                    Success = false,
                    Message = "Failed to add favorite to meal",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}