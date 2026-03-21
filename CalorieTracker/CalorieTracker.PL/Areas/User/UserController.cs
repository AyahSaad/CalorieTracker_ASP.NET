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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private string GetUserId() =>User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _userService.GetProfileAsync(GetUserId());
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPatch("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileRequest request)
        {
            var result = await _userService.UpdateProfileAsync(GetUserId(), request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPatch("calorie-goal")]
        public async Task<IActionResult> UpdateCalorieGoal(
            [FromBody] UpdateCalorieGoalRequest request)
        {
            var result = await _userService.UpdateCalorieGoalAsync(GetUserId(), request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
