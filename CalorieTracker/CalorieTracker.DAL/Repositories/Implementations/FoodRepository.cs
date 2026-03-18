using CalorieTracker.DAL.Data;
using CalorieTracker.DAL.Models;
using CalorieTracker.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.DAL.Repository.Implementations
{
    public class FoodRepository : IFoodRepository
    {
        private readonly ApplicationDbContext _context;

        public FoodRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Food> Query()
        {
            return _context.Foods.Include(f => f.Measures).AsQueryable();
        }
        public async Task<Food> AddAsync(Food food)
        {
            await _context.Foods.AddAsync(food);
            await _context.SaveChangesAsync();
            return food;
        }

        public async Task<Food?> GetByIdAsync(int id)
        {
            return await _context.Foods
                .Include(f => f.Measures)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Food?> GetByExternalIdAsync(string externalId)
        {
            return await _context.Foods
                .Include(f => f.Measures)
                .FirstOrDefaultAsync(f => f.ExternalId == externalId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food is null) return false;

            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAllAsync()
        {
            var foods = await _context.Foods.ToListAsync();
            if (!foods.Any()) return false;

            _context.Foods.RemoveRange(foods);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(string externalId)
        {
            return await _context.Foods
                .AnyAsync(f => f.ExternalId == externalId);
        }        
    }
}