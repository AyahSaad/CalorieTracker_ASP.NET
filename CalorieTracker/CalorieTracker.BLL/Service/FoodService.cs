using CalorieTracker.BLL.Interfaces;
using CalorieTracker.DAL.DTO.Request;
using CalorieTracker.DAL.DTO.Response;
using CalorieTracker.DAL.Models;
using CalorieTracker.DAL.Repositories.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.BLL.Service
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IEdamamService _edamamService;

        public FoodService(IFoodRepository foodRepository,IEdamamService edamamService)
        {
            _foodRepository = foodRepository;
            _edamamService = edamamService;
        }

        public async Task<BaseResponse<FoodResponse>> AddManualFoodAsync(AddFoodRequest request)
        {
            try
            {
                var food = request.Adapt<Food>();

                // Default Measures 
                food.Measures.Add(new FoodMeasure { MeasureName = "Gram", WeightInGrams = 1 });
                food.Measures.Add(new FoodMeasure { MeasureName = "100g", WeightInGrams = 100 });

                var saved = await _foodRepository.AddAsync(food);

                return new BaseResponse<FoodResponse>
                {
                    Success = true,
                    Message = "Food added successfully",
                    Data = saved.Adapt<FoodResponse>()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<FoodResponse>
                {
                    Success = false,
                    Message = "Failed to add food",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<PaginatedResponse<FoodResponse>> SearchFoodAsync(string name, int page = 1, int limit = 10)
        {
            try
            {
                var query = _foodRepository.Query()
                    .Where(f => EF.Functions.Like(f.Name, $"%{name}%"));

                var totalCount = await query.CountAsync();
                var localResults = await query
                    .Skip((page - 1) * limit)
                    .Take(limit)
                    .ToListAsync();

                // DB
                if (localResults.Any())
                    return new PaginatedResponse<FoodResponse>
                    {
                        TotalCount = totalCount,
                        Page = page,
                        Limit = limit,
                        Data = localResults.Adapt<List<FoodResponse>>()
                    };

                // Edamam API
                var apiResults = await _edamamService.SearchFromApiAsync(name);
                return new PaginatedResponse<FoodResponse>
                {
                    TotalCount = apiResults.Count,
                    Page = 1,
                    Limit = apiResults.Count,
                    Data = apiResults
                };
            }
            catch (Exception ex)
            {
                return new PaginatedResponse<FoodResponse>
                {
                    TotalCount = 0,
                    Page = page,
                    Limit = limit,
                    Data = new List<FoodResponse>()
                };
            }
        }
        public async Task<BaseResponse<FoodResponse>> GetFoodByIdAsync(int id)
        {
            try
            {
                var food = await _foodRepository.GetByIdAsync(id);
                if (food is null)
                {
                    return new BaseResponse<FoodResponse>
                    {
                        Success = false,
                        Message = "Food not found"
                    };
                }

                return new BaseResponse<FoodResponse>
                {
                    Success = true,
                    Message = "Food retrieved successfully",
                    Data = food.Adapt<FoodResponse>()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<FoodResponse>
                {
                    Success = false,
                    Message = "Failed to get food",
                    Errors = new List<string> { ex.Message }
                };
            }
        }


        public async Task<PaginatedResponse<FoodResponse>> GetAllFoodsAsync( int page = 1, int limit = 10)
        {
            try
            {
                var query = _foodRepository.Query();

                var totalCount = await query.CountAsync();
                var foods = await query
                    .Skip((page - 1) * limit)
                    .Take(limit)
                    .ToListAsync();

                return new PaginatedResponse<FoodResponse>
                {
                    TotalCount = totalCount,
                    Page = page,
                    Limit = limit,
                    Data = foods.Adapt<List<FoodResponse>>()
                };
            }
            catch (Exception ex)
            {
                return new PaginatedResponse<FoodResponse>
                {
                    TotalCount = 0,
                    Page = page,
                    Limit = limit,
                    Data = new List<FoodResponse>()
                };
            }
        }

        public async Task<BaseResponse> DeleteFoodAsync(int id)
        {
            try
            {
                var result = await _foodRepository.DeleteAsync(id);
                if (!result)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Food not found"
                    };
                }

                return new BaseResponse
                {
                    Success = true,
                    Message = "Food deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Failed to delete food",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseResponse> DeleteAllFoodsAsync()
        {
            try
            {
                var count = await _foodRepository.Query().CountAsync();
                if (count == 0)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "No foods found to delete"
                    };
                }

                await _foodRepository.DeleteAllAsync();
                return new BaseResponse
                {
                    Success = true,
                    Message = $"{count} foods deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Failed to delete all foods",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}