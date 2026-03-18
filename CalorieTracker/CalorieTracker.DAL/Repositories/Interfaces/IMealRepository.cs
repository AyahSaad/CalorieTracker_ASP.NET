using CalorieTracker.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.Repositories.Interfaces
{
    public interface IMealRepository
    {
        // IQueryable for Filter
        IQueryable<Meal> Query();
        Task<Meal> AddAsync(Meal meal);
        Task<Meal?> GetByIdAsync(int id);
        Task<Meal?> GetUserMealAsync(int mealId, string userId);
        Task<bool> DeleteAsync(int id);
        Task<MealFood> AddFoodToMealAsync(MealFood mealFood);
        Task<bool> RemoveFoodFromMealAsync(int mealFoodId);
        Task<MealFood?> GetMealFoodByIdAsync(int mealFoodId);
    }
}
