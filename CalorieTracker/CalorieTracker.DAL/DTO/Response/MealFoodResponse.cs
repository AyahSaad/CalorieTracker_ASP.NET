using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Response
{
    public class MealFoodResponse
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public string FoodName { get; set; } = string.Empty;
        public string? FoodImageUrl { get; set; }
        public float Quantity { get; set; }
        public string MeasureUnit { get; set; } = string.Empty;
        public float TotalWeightInGrams { get; set; }
        public float TotalCalories { get; set; }
        public float TotalProtein { get; set; }
        public float TotalFat { get; set; }
        public float TotalCarbs { get; set; }
    }
}
