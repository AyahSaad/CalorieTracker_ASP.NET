using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Request
{
    public class AddFoodMeasureRequest
    {
        public string MeasureName { get; set; } = string.Empty;
        public float WeightInGrams { get; set; }
    }
}
