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
        // IQueryable for Filter and Pagination   
        IQueryable<Food> Query();
        Task<Food> AddAsync(Food food);
        Task<Food?> GetByIdAsync(int id);
        Task<Food?> GetByExternalIdAsync(string externalId);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAllAsync();
        Task<bool> ExistsAsync(string externalId);
    }
}
