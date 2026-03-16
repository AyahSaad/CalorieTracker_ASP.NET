using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.Models
{
    public class Food : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public float CaloriesPer100g { get; set; }
        public float ProteinPer100g { get; set; }
        public float FatPer100g { get; set; }
        public float CarbsPer100g { get; set; }
        public float FiberPer100g { get; set; }
        public string Source { get; set; } = string.Empty;
        public string? ExternalId { get; set; }
        public string? ImageUrl { get; set; }

        // Navigation Properties
        public ICollection<FoodMeasure> Measures { get; set; } = new List<FoodMeasure>();
        public ICollection<MealFood> MealFoods { get; set; } = new List<MealFood>();
        public ICollection<FavoriteFood> FavoriteFoods { get; set; } = new List<FavoriteFood>();
    }
}
