using CalorieTracker.DAL.Data;
using CalorieTracker.DAL.Models;
using CalorieTracker.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.DAL.Repository.Implementations
{
    public class MealRepository : IMealRepository
    {
        private readonly ApplicationDbContext _context;

        public MealRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Meal> Query()
        {
            return _context.Meals.Include(m => m.MealFoods).ThenInclude(mf => mf.Food).AsQueryable();
        }

        public async Task<Meal> AddAsync(Meal meal)
        {
            await _context.Meals.AddAsync(meal);
            await _context.SaveChangesAsync();
            return meal;
        }

        public async Task<Meal?> GetByIdAsync(int id)
        {
            return await _context.Meals
                .Include(m => m.MealFoods)
                    .ThenInclude(mf => mf.Food)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Meal?> GetUserMealAsync(int mealId, string userId)
        {
            return await _context.Meals
                .Include(m => m.MealFoods)
                    .ThenInclude(mf => mf.Food)
                .FirstOrDefaultAsync(m => m.Id == mealId &&
                                          m.UserId == userId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal is null) return false;

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<MealFood> AddFoodToMealAsync(MealFood mealFood)
        {
            await _context.MealFoods.AddAsync(mealFood);
            await _context.SaveChangesAsync();
            return mealFood;
        }

        public async Task<bool> RemoveFoodFromMealAsync(int mealFoodId)
        {
            var mealFood = await _context.MealFoods.FindAsync(mealFoodId);
            if (mealFood is null) return false;

            _context.MealFoods.Remove(mealFood);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<MealFood?> GetMealFoodByIdAsync(int mealFoodId)
        {
            return await _context.MealFoods.Include(mf => mf.Food)
                .FirstOrDefaultAsync(mf => mf.Id == mealFoodId);
        }

        public async Task<Meal> UpdateAsync(Meal meal)
        {
            _context.Meals.Update(meal);
            await _context.SaveChangesAsync();
            return meal;
        }

        public async Task<bool> DeleteAllByDateAsync(string userId, DateTime date)
        {
            var meals = await _context.Meals.Where(m => m.UserId == userId && m.Date.Date == date.Date).ToListAsync();
            if (!meals.Any()) return false;
            _context.Meals.RemoveRange(meals);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Meal>> GetAllByUserAsync(string userId)
        {
            return await _context.Meals.Include(m => m.MealFoods)
                .ThenInclude(mf => mf.Food)
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }
    }
}