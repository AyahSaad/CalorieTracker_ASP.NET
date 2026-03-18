using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Request
{
    public class AddFoodToMealRequest
    {
        [Required]
        public int FoodId { get; set; }

        [Required]
        public int MeasureId { get; set; }

        [Range(0.1, 1000, ErrorMessage = "Quantity must be between 0.1 and 1000")]
        public float Quantity { get; set; }
    }
}
