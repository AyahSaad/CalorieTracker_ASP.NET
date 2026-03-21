using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Response
{
    public class WeightProgressResponse
    {
        public float FirstWeight { get; set; }
        public float LastWeight { get; set; }
        public float TotalChange { get; set; }
        public string ChangeDirection { get; set; } = string.Empty;
        public List<WeightLogResponse> Logs { get; set; } = new();
    }
}