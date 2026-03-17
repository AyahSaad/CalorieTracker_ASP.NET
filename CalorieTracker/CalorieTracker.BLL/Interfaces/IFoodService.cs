using CalorieTracker.DAL.DTO.Request;
using CalorieTracker.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.BLL.Interfaces
{
    public interface IFoodService
    {
        Task<BaseResponse<FoodResponse>> AddManualFoodAsync(AddFoodRequest request);
        Task<PaginatedResponse<FoodResponse>> SearchFoodAsync(string name, int page = 1, int limit = 10);
        Task<BaseResponse<FoodResponse>> GetFoodByIdAsync(int id);
        Task<PaginatedResponse<FoodResponse>> GetAllFoodsAsync(int page = 1, int limit = 10);
        Task<BaseResponse> DeleteFoodAsync(int id);
        Task<BaseResponse> DeleteAllFoodsAsync();
    }
}
