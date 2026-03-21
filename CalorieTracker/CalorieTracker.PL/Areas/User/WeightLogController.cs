using CalorieTracker.BLL.Interfaces;
using CalorieTracker.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CalorieTracker.PL.Areas.User
{
    [Route("api/user/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class WeightLogController : ControllerBase
    {
        private readonly IWeightLogService _weightLogService;

        public WeightLogController(IWeightLogService weightLogService)
        {
            _weightLogService = weightLogService;
        }

        private string GetUserId() =>User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpPost]
        public async Task<IActionResult> AddWeightLog(
            [FromBody] AddWeightLogRequest request)
        {
            var result = await _weightLogService.AddWeightLogAsync(GetUserId(), request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogs()
        {
            var result = await _weightLogService.GetAllLogsAsync(GetUserId());
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpGet("progress")]
        public async Task<IActionResult> GetProgress()
        {
            var result = await _weightLogService.GetProgressAsync(GetUserId());
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeightLog([FromRoute] int id)
        {
            var result = await _weightLogService.DeleteWeightLogAsync(GetUserId(), id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }
    }
}
