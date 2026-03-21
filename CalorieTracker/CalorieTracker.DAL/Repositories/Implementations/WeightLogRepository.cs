using CalorieTracker.DAL.Data;
using CalorieTracker.DAL.Models;
using CalorieTracker.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.DAL.Repository.Implementations
{
    public class WeightLogRepository : IWeightLogRepository
    {
        private readonly ApplicationDbContext _context;

        public WeightLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<WeightLog> Query()
        {
            return _context.WeightLogs.AsQueryable();
        }

        public async Task<WeightLog> AddAsync(WeightLog weightLog)
        {
            await _context.WeightLogs.AddAsync(weightLog);
            await _context.SaveChangesAsync();
            return weightLog;
        }

        public async Task<WeightLog?> GetByIdAsync(int id)
        {
            return await _context.WeightLogs
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var log = await _context.WeightLogs.FindAsync(id);
            if (log is null) return false;

            _context.WeightLogs.Remove(log);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}