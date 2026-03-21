using CalorieTracker.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CalorieTracker.PL.Areas.User
{
    [Route("api/user/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        //Daily Report
        [HttpGet("daily")]
        public async Task<IActionResult> GetDailyReport([FromQuery] DateTime date)
        {
            var result = await _reportService.GetDailyReportAsync(GetUserId(), date);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        //Weekly Report
        [HttpGet("weekly")]
        public async Task<IActionResult> GetWeeklyReport([FromQuery] DateTime startDate)
        {
            var result = await _reportService.GetWeeklyReportAsync(GetUserId(), startDate);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
