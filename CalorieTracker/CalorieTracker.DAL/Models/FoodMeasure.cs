using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.Models
{
    public class FoodMeasure
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public string MeasureName { get; set; } = string.Empty;
        public float WeightInGrams { get; set; }
        public Food Food { get; set; } = null!;
    }
}
