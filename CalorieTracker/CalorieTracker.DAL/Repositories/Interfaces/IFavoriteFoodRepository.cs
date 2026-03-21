using CalorieTracker.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.Repositories.Interfaces
{
    public interface IFavoriteFoodRepository
    {
        IQueryable<FavoriteFood> Query();
        Task<FavoriteFood> AddAsync(FavoriteFood favoriteFood);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(string userId, int foodId);
    }
}
