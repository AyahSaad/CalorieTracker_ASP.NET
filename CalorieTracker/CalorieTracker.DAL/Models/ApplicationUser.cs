using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; }
        public string? City {  get; set; }
        public string? Street { get; set; }
        public string? CodeResetPassword { get; set; }
        public DateTime? PasswordResetCodeExpiry { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public float DailyCalorieGoal { get; set; } = 2000;
        public float? CurrentWeight { get; set; }
        public float? Height { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }

        // Navigation Properties
        public ICollection<Meal> Meals { get; set; } = new List<Meal>();
        public ICollection<WeightLog> WeightLogs { get; set; } = new List<WeightLog>();
        public ICollection<FavoriteFood> FavoriteFoods { get; set; } = new List<FavoriteFood>();

    }
}
