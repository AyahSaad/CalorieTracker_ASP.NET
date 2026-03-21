using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.DAL.DTO.Response
{
    public class WeeklyReportResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float DailyCalorieGoal { get; set; }
        public float TotalCaloriesConsumed { get; set; }
        public float AverageCaloriesPerDay { get; set; }
        public List<DailyReportResponse> DailyReports { get; set; } = new();
    }
}
