using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Response
{
    public class FoodResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float CaloriesPer100g { get; set; }
        public float ProteinPer100g { get; set; }
        public float FatPer100g { get; set; }
        public float CarbsPer100g { get; set; }
        public float FiberPer100g { get; set; }
        public string Source { get; set; }
        public string? ImageUrl { get; set; }
        public List<FoodMeasureResponse> Measures { get; set; } = new();
    }
}
