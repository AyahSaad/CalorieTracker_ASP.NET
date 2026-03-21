using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Request
{
    public class UpdateUserProfileRequest
    {
            [StringLength(100)]
            public string? FullName { get; set; }
            public string? City { get; set; }
            public string? Street { get; set; }

            [Range(0, 500)]
            public float? Height { get; set; }

            [Range(1, 120)]
            public int? Age { get; set; }
            public string? Gender { get; set; }
        }
}
