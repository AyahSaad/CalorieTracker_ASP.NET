using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Response
{
    public class UserProfileResponse
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public float DailyCalorieGoal { get; set; }
        public float? CurrentWeight { get; set; }
        public float? Height { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
    }
}
