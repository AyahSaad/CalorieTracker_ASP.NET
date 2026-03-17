using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace CalorieTracker.DAL.DTO.Request
{
    public class AddFoodRequest
    {

        [Required(ErrorMessage = "Food name is required")]
        [StringLength(100, ErrorMessage = "Name too long")]
        public string Name { get; set; }

        [Range(0, 1000, ErrorMessage = "Calories must be between 0 and 1000")]
        public float CaloriesPer100g { get; set; }

        [Range(0, 100, ErrorMessage = "Protein must be between 0 and 100")]
        public float ProteinPer100g { get; set; }

        [Range(0, 100, ErrorMessage = "Fat must be between 0 and 100")]
        public float FatPer100g { get; set; }

        [Range(0, 100, ErrorMessage = "Carbs must be between 0 and 100")]
        public float CarbsPer100g { get; set; }

        [Range(0, 100, ErrorMessage = "Fiber must be between 0 and 100")]
        public float FiberPer100g { get; set; }

        public List<AddFoodMeasureRequest> Measures { get; set; } = new();
    
   }
}
