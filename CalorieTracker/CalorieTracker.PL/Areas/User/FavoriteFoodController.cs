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

    public class FavoriteFoodController : ControllerBase
    {
        private readonly IFavoriteFoodService _favoriteFoodService;

        public FavoriteFoodController(IFavoriteFoodService favoriteFoodService)
        {
            _favoriteFoodService = favoriteFoodService;
        }

        private string GetUserId() =>User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] AddFavoriteFoodRequest request)
        {
            var result = await _favoriteFoodService
                .AddFavoriteAsync(GetUserId(), request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFavorites()
        {
            var result = await _favoriteFoodService
                .GetAllFavoritesAsync(GetUserId());
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite([FromRoute] int id)
        {
            var result = await _favoriteFoodService
                .DeleteFavoriteAsync(GetUserId(), id);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost("{favoriteId}/add-to-meal/{mealId}")]
        public async Task<IActionResult> AddFavoriteToMeal([FromRoute] int favoriteId, [FromRoute] int mealId, [FromBody] AddFoodToMealRequest request)
        {
            var result = await _favoriteFoodService
                .AddFavoriteToMealAsync(GetUserId(), favoriteId, mealId, request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}