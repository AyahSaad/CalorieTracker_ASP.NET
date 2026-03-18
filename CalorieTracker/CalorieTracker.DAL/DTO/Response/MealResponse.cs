using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Response
{
    public class MealResponse
    {
        public int Id { get; set; }
        public string MealType { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public float TotalCalories { get; set; }
        public List<MealFoodResponse> Foods { get; set; } = new();
    }
}

