using CalorieTracker.BLL.Interfaces;
using CalorieTracker.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.PL.Areas.User
{
    [Route("api/user/[controller]")]
    [ApiController]
    [Authorize]
    public class FoodController : ControllerBase
    {
        private readonly IFoodService _foodService;

        public FoodController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpPost("add-manual")]
        public async Task<IActionResult> AddManualFood([FromBody] AddFoodRequest request)
        {
            var result = await _foodService.AddManualFoodAsync(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchFood([FromQuery] string name,[FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Search term is required");

            var result = await _foodService.SearchFoodAsync(name, page, limit);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFoodById([FromRoute] int id)
        {
            var result = await _foodService.GetFoodByIdAsync(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllFoods([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var result = await _foodService.GetAllFoodsAsync(page, limit);
            return Ok(result);
        }

        [HttpDelete("deleteall")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> DeleteAllFoods()
        {
            var result = await _foodService.DeleteAllFoodsAsync();
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> DeleteFood([FromRoute] int id)
        {
            var result = await _foodService.DeleteFoodAsync(id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }
    }
}
