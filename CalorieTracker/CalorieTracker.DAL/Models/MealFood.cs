using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.Models
{
    public class MealFood
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public int FoodId { get; set; }
        public float Quantity { get; set; }
        public string MeasureUnit { get; set; } = string.Empty;
        public float TotalWeightInGrams { get; set; }
        public float TotalCalories { get; set; }

        // Navigation Properties
        public Meal Meal { get; set; } = null!;
        public Food Food { get; set; } = null!;
    }
}
