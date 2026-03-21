using CalorieTracker.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Request
{
    public class UpdateMealRequest
    {
        [Required]
        public MealType MealType { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
