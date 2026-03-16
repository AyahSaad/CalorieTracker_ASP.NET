using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.Models
{
    public class WeightLog : BaseModel
    {
        public string UserId { get; set; } = string.Empty;
        public float WeightInKg { get; set; }
        public DateTime LoggedAt { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }

        // Navigation Property
        public ApplicationUser User { get; set; } = null!;
    }
}
