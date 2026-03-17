using CalorieTracker.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.BLL.Interfaces
{
    public interface IEdamamService
    {
        Task<List<FoodResponse>> SearchFromApiAsync(string name);
    }
}
