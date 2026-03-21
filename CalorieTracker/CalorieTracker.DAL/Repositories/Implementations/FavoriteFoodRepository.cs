using CalorieTracker.DAL.Data;
using CalorieTracker.DAL.Models;
using CalorieTracker.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.DAL.Repository.Implementations
{
    public class FavoriteFoodRepository : IFavoriteFoodRepository
    {
        private readonly ApplicationDbContext _context;

        public FavoriteFoodRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<FavoriteFood> Query()
        {
            return _context.FavoriteFoods
                .Include(f => f.Food)
                    .ThenInclude(f => f.Measures)
                .AsQueryable();
        }

        public async Task<FavoriteFood> AddAsync(FavoriteFood favoriteFood)
        {
            await _context.FavoriteFoods.AddAsync(favoriteFood);
            await _context.SaveChangesAsync();
            return favoriteFood;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var favorite = await _context.FavoriteFoods.FindAsync(id);
            if (favorite is null) return false;

            _context.FavoriteFoods.Remove(favorite);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(string userId, int foodId)
        {
            return await _context.FavoriteFoods
                .AnyAsync(f => f.UserId == userId &&
                               f.FoodId == foodId);
        }
    }
}