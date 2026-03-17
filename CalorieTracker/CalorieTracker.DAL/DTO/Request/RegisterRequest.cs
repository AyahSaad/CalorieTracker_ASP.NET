using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Request
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber  { get; set; }
        public float DailyCalorieGoal { get; set; } = 2000;
        public float? CurrentWeight { get; set; }
        public float? Height { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
    }
}
