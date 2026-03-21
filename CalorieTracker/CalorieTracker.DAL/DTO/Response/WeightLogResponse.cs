using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Response
{
    public class WeightLogResponse
    {
        public int Id { get; set; }
        public float WeightInKg { get; set; }
        public DateTime LoggedAt { get; set; }
        public string? Notes { get; set; }
    }
}
