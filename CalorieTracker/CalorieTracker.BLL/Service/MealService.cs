using CalorieTracker.BLL.Interfaces;
using CalorieTracker.DAL.DTO.Request;
using CalorieTracker.DAL.DTO.Response;
using CalorieTracker.DAL.Models;
using CalorieTracker.DAL.Repositories.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.BLL.Service
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;
        private readonly IFoodRepository _foodRepository;

        public MealService(IMealRepository mealRepository, IFoodRepository foodRepository)
        {
            _mealRepository = mealRepository;
            _foodRepository = foodRepository;
        }

        public async Task<BaseResponse<MealResponse>> CreateMealAsync(
            string userId, AddMealRequest request)
        {
            try
            {
                var meal = new Meal
                {
                    UserId = userId,
                    MealType = request.MealType,
                    Date = request.Date.Date
                };

                var saved = await _mealRepository.AddAsync(meal);
                return new BaseResponse<MealResponse>
                {
                    Success = true,
                    Message = "Meal created successfully",
                    Data = saved.Adapt<MealResponse>()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<MealResponse>
                {
                    Success = false,
                    Message = "Failed to create meal",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse<MealFoodResponse>> AddFoodToMealAsync(
            string userId, int mealId, AddFoodToMealRequest request)
        {
            try
            {
                var meal = await _mealRepository.GetUserMealAsync(mealId, userId);
                if (meal is null)
                    return new BaseResponse<MealFoodResponse>
                    {
                        Success = false,
                        Message = "Meal not found"
                    };

                var food = await _foodRepository.GetByIdAsync(request.FoodId);
                if (food is null)
                    return new BaseResponse<MealFoodResponse>
                    {
                        Success = false,
                        Message = "Food not found"
                    };

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
                    FoodId             = request.FoodId,
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
                    Message = "Food added to meal successfully",
                    Data = saved.Adapt<MealFoodResponse>()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<MealFoodResponse>
                {
                    Success = false,
                    Message = "Failed to add food to meal",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse<List<MealResponse>>> GetMealsByDateAsync(string userId, DateTime date)
        {
            try
            {
                var meals = await _mealRepository.Query()
                    .Where(m => m.UserId == userId &&
                                m.Date.Date == date.Date)
                    .ToListAsync();

                if (!meals.Any())
                    return new BaseResponse<List<MealResponse>>
                    {
                        Success = false,
                        Message = $"No meals found for {date:yyyy-MM-dd}",
                        Data = new List<MealResponse>()
                    };

                return new BaseResponse<List<MealResponse>>
                {
                    Success = true,
                    Message = "Meals retrieved successfully",
                    Data = meals.Adapt<List<MealResponse>>()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<MealResponse>>
                {
                    Success = false,
                    Message = "Failed to get meals",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse<MealResponse>> GetMealByIdAsync(
            string userId, int mealId)
        {
            try
            {
                var meal = await _mealRepository.GetUserMealAsync(mealId, userId);
                if (meal is null)
                    return new BaseResponse<MealResponse>
                    {
                        Success = false,
                        Message = "Meal not found"
                    };

                return new BaseResponse<MealResponse>
                {
                    Success = true,
                    Message = "Meal retrieved successfully",
                    Data = meal.Adapt<MealResponse>()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<MealResponse>
                {
                    Success = false,
                    Message = "Failed to get meal",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse> DeleteMealAsync(string userId, int mealId)
        {
            try
            {
                var meal = await _mealRepository.GetUserMealAsync(mealId, userId);
                if (meal is null)
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Meal not found"
                    };

                await _mealRepository.DeleteAsync(mealId);
                return new BaseResponse
                {
                    Success = true,
                    Message = "Meal deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Failed to delete meal",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse> RemoveFoodFromMealAsync(string userId, int mealFoodId)
        {
            try
            {
                var mealFood = await _mealRepository.GetMealFoodByIdAsync(mealFoodId);
                if (mealFood is null)
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Food not found in meal"
                    };

                var meal = await _mealRepository.GetUserMealAsync(
                    mealFood.MealId, userId);
                if (meal is null)
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Unauthorized"
                    };

                await _mealRepository.RemoveFoodFromMealAsync(mealFoodId);
                return new BaseResponse
                {
                    Success = true,
                    Message = "Food removed from meal successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Failed to remove food from meal",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse<MealResponse>> UpdateMealAsync(string userId, int mealId, UpdateMealRequest request)
        {
            try
            {
                var meal = await _mealRepository.GetUserMealAsync(mealId, userId);
                if (meal is null)
                    return new BaseResponse<MealResponse>
                    {
                        Success = false,
                        Message = "Meal not found"
                    };

                meal.MealType = request.MealType;
                meal.Date = request.Date.Date;

                var updated = await _mealRepository.UpdateAsync(meal);
                return new BaseResponse<MealResponse>
                {
                    Success = true,
                    Message = "Meal updated successfully",
                    Data = updated.Adapt<MealResponse>()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<MealResponse>
                {
                    Success = false,
                    Message = "Failed to update meal",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse> DeleteAllMealsByDateAsync(string userId, DateTime date)
        {
            try
            {
                var result = await _mealRepository.DeleteAllByDateAsync(userId, date);
                if (!result)
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "No meals found for this date"
                    };

                return new BaseResponse
                {
                    Success = true,
                    Message = "All meals deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Failed to delete meals",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse<List<MealResponse>>> GetAllMealsAsync(string userId)
        {
            try
            {
                var meals = await _mealRepository.GetAllByUserAsync(userId);

                if (!meals.Any())
                    return new BaseResponse<List<MealResponse>>
                    {
                        Success = false,
                        Message = "No meals found",
                        Data = new List<MealResponse>()
                    };

                return new BaseResponse<List<MealResponse>>
                {
                    Success = true,
                    Message = "Meals retrieved successfully",
                    Data = meals.Adapt<List<MealResponse>>()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<MealResponse>>
                {
                    Success = false,
                    Message = "Failed to get meals",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}