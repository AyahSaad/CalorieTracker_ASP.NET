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

        public async Task<List<Food>> SearchLocalAsync(string name, int page, int limit)
        {
            return await _context.Foods
                .Include(f => f.Measures)
                .Where(f => EF.Functions.Like(f.Name, $"%{name}%"))
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<int> CountSearchAsync(string name)
        {
            return await _context.Foods
                .Where(f => EF.Functions.Like(f.Name, $"%{name}%"))
                .CountAsync();
        }

        public async Task<List<Food>> GetAllAsync(int page, int limit)
        {
            return await _context.Foods
                .Include(f => f.Measures)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<int> CountAllAsync()
        {
            return await _context.Foods.CountAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food is null) return false;

            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(string externalId)
        {
            return await _context.Foods
                .AnyAsync(f => f.ExternalId == externalId);
        }

        public async Task DeleteAllAsync()
        {
            _context.FoodMeasures.RemoveRange(_context.FoodMeasures);
            _context.Foods.RemoveRange(_context.Foods);
            await _context.SaveChangesAsync();
        }
    }
}