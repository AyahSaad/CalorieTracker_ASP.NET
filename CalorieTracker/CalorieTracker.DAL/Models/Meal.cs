using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.Models
{
    public class Meal : BaseModel
    {
        public string UserId { get; set; } = string.Empty;
        public MealType MealType { get; set; }
        public DateTime Date { get; set; }

        // Navigation Properties
        public ApplicationUser User { get; set; } = null!;
        public ICollection<MealFood> MealFoods { get; set; } = new List<MealFood>();
    }
}
