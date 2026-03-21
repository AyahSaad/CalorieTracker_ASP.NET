using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Request
{
    public class AddFavoriteFoodRequest
    {
        [Required]
        public int FoodId { get; set; }
    }
}
