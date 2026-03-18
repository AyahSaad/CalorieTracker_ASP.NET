using CalorieTracker.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Request
{
    public class AddMealRequest
    {
        [Required(ErrorMessage = "Meal type is required")]
        public MealType MealType { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }
    }
}
