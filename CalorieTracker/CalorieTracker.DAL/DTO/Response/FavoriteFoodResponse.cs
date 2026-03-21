using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Response
{
    public class FavoriteFoodResponse
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public string FoodName { get; set; } = string.Empty;
        public float CaloriesPer100g { get; set; }
        public string? ImageUrl { get; set; }
        public List<FoodMeasureResponse> Measures { get; set; } = new();
    }
}
