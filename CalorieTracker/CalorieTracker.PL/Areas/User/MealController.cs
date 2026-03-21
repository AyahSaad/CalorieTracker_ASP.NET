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

    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;

        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }

        //  UserId من الـ Token
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpPost]
        public async Task<IActionResult> CreateMeal([FromBody] AddMealRequest request)
        {
            var result = await _mealService.CreateMealAsync(GetUserId(), request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("{mealId}/foods")]
        public async Task<IActionResult> AddFoodToMeal([FromRoute] int mealId,[FromBody] AddFoodToMealRequest request)
        {
            var result = await _mealService.AddFoodToMealAsync(GetUserId(), mealId, request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("by-date")]
        public async Task<IActionResult> GetMealsByDate([FromQuery] DateTime date)
        {
            var result = await _mealService.GetMealsByDateAsync(GetUserId(), date);
            return Ok(result);
        }

        [HttpGet("{mealId}")]
        public async Task<IActionResult> GetMealById([FromRoute] int mealId)
        {
            var result = await _mealService.GetMealByIdAsync(GetUserId(), mealId);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{mealId}")]
        public async Task<IActionResult> DeleteMeal([FromRoute] int mealId)
        {
            var result = await _mealService.DeleteMealAsync(GetUserId(), mealId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("foods/{mealFoodId}")]
        public async Task<IActionResult> RemoveFoodFromMeal([FromRoute] int mealFoodId)
        {
            var result = await _mealService.RemoveFoodFromMealAsync(GetUserId(), mealFoodId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPatch("{mealId}")]
        public async Task<IActionResult> UpdateMeal([FromRoute] int mealId, [FromBody] UpdateMealRequest request)
        {
            var result = await _mealService.UpdateMealAsync(GetUserId(), mealId, request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("by-date")]
        public async Task<IActionResult> DeleteAllMealsByDate([FromQuery] DateTime date)
        {
            var result = await _mealService.DeleteAllMealsByDateAsync(GetUserId(), date);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMeals()
        {
            var result = await _mealService.GetAllMealsAsync(GetUserId());
            return Ok(result);
        }
    }
}
