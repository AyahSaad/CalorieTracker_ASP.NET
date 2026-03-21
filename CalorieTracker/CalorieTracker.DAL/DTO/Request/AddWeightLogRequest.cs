using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Request
{
    public class AddWeightLogRequest
    {
        [Range(20, 300, ErrorMessage = "Weight must be between 20 and 300 kg")]
        public float WeightInKg { get; set; }
        public DateTime LoggedAt { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }
    }
}
