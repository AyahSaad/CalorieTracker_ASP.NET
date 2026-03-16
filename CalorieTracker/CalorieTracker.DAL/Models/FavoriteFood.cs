using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.Models
{
    public class FavoriteFood
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int FoodId { get; set; }

        // Navigation Properties
        public ApplicationUser User { get; set; } = null!;
        public Food Food { get; set; } = null!;
    }
}
