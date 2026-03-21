using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Response
{
    public class DailyReportResponse
    {
        public DateTime Date { get; set; }
        public float DailyCalorieGoal { get; set; }
        public float TotalCaloriesConsumed { get; set; }
        public float RemainingCalories { get; set; }
        public float TotalProtein { get; set; }
        public float TotalFat { get; set; }
        public float TotalCarbs { get; set; }
        public List<MealResponse> Meals { get; set; } = new();
    }
}
