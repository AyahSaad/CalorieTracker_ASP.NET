using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Response
{
    public class FoodMeasureResponse
    {
        public int Id { get; set; }
        public string MeasureName { get; set; }
        public float WeightInGrams { get; set; }
    }
}
