using CalorieTracker.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.Repositories.Interfaces
{
    public interface IFoodRepository
    {
        Task<Food> AddAsync(Food food);
        Task<Food?> GetByIdAsync(int id);
        Task<Food?> GetByExternalIdAsync(string externalId);
        Task<List<Food>> SearchLocalAsync(string name, int page, int limit);
        Task<int> CountSearchAsync(string name);
        Task<List<Food>> GetAllAsync(int page, int limit);
        Task<int> CountAllAsync();
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(string externalId);
        Task DeleteAllAsync(); 
    }
}
