using CalorieTracker.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.Repositories.Interfaces
{
    public interface IWeightLogRepository
    {
        IQueryable<WeightLog> Query();
        Task<WeightLog> AddAsync(WeightLog weightLog);
        Task<WeightLog?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
