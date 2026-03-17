using CalorieTracker.BLL.Interfaces;
using CalorieTracker.DAL.DTO.Response;
using CalorieTracker.DAL.Models;
using CalorieTracker.DAL.Repositories.Interfaces;
using Mapster;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CalorieTracker.BLL.Service
{
    public class EdamamService : IEdamamService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IFoodRepository _foodRepository;

        public EdamamService(
            HttpClient httpClient,
            IConfiguration configuration,
            IFoodRepository foodRepository)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _foodRepository = foodRepository;
        }

        public async Task<List<FoodResponse>> SearchFromApiAsync(string name)
        {
            var appId = _configuration["Edamam:AppId"];
            var appKey = _configuration["Edamam:AppKey"];
            var url = $"https://api.edamam.com/api/food-database/v2/parser?ingr={name}&app_id={appId}&app_key={appKey}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return new List<FoodResponse>();

            var json = await response.Content.ReadAsStringAsync();
            var root = JsonDocument.Parse(json).RootElement;
            var results = new List<FoodResponse>();

            if (!root.TryGetProperty("hints", out var hints))
                return results;

            var processedIds = new HashSet<string>();

            foreach (var hint in hints.EnumerateArray().Take(10))
            {
                var foodElement = hint.GetProperty("food");
                var nutrients = foodElement.GetProperty("nutrients");  
                var externalId = foodElement.GetProperty("foodId").GetString()!;

                if (!processedIds.Add(externalId))
                    continue;

                var existingFood = await _foodRepository.GetByExternalIdAsync(externalId);
                if (existingFood is not null)
                {
                    results.Add(existingFood.Adapt<FoodResponse>());
                    continue;
                }

                var newFood = new Food
                {
                    Name = foodElement.GetProperty("label").GetString()!,
                    CaloriesPer100g = nutrients.TryGetProperty("ENERC_KCAL", out var cal)
                        ? cal.GetSingle() : 0,
                    ProteinPer100g = nutrients.TryGetProperty("PROCNT", out var pro)
                        ? pro.GetSingle() : 0,
                    FatPer100g = nutrients.TryGetProperty("FAT", out var fat)
                        ? fat.GetSingle() : 0,
                    CarbsPer100g = nutrients.TryGetProperty("CHOCDF", out var carb)
                        ? carb.GetSingle() : 0,
                    FiberPer100g = nutrients.TryGetProperty("FIBTG", out var fib)
                        ? fib.GetSingle() : 0,
                    Source = "Edamam",
                    ExternalId = externalId,
                    ImageUrl = foodElement.TryGetProperty("image", out var img)
                        ? img.GetString() : null
                };

                if (hint.TryGetProperty("measures", out var measures))
                {
                    foreach (var measure in measures.EnumerateArray())
                    {
                        newFood.Measures.Add(new FoodMeasure
                        {
                            MeasureName = measure.GetProperty("label").GetString()!,
                            WeightInGrams = measure.GetProperty("weight").GetSingle()
                        });
                    }
                }

                var saved = await _foodRepository.AddAsync(newFood);
                results.Add(saved.Adapt<FoodResponse>());
            }

            return results;
        }
    }
}