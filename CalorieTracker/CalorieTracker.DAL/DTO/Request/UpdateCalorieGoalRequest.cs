using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Request
{
    public class UpdateCalorieGoalRequest
    {
        [Range(500, 10000, ErrorMessage = "Calorie goal must be between 500 and 10000")]
        public float DailyCalorieGoal { get; set; }
    }
}