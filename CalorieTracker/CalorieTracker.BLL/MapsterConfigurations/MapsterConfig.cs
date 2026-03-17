using CalorieTracker.DAL.DTO.Request;
using CalorieTracker.DAL.DTO.Response;
using CalorieTracker.DAL.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.BLL.MapsterConfigurations
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            // AddFoodRequest → Food
            TypeAdapterConfig<AddFoodRequest, Food>
                .NewConfig()
                .Map(dest => dest.Source, src => "Manual")
                .Map(dest => dest.ExternalId, src => (string?)null)
                .Map(dest => dest.Measures,
                     src => src.Measures.Adapt<List<FoodMeasure>>());

            // AddFoodMeasureRequest → FoodMeasure
            TypeAdapterConfig<AddFoodMeasureRequest, FoodMeasure>
                .NewConfig();

            // Food → FoodResponse
            TypeAdapterConfig<Food, FoodResponse>
                .NewConfig()
                .Map(dest => dest.Measures,
                     src => src.Measures.Adapt<List<FoodMeasureResponse>>());

            // FoodMeasure → FoodMeasureResponse
            TypeAdapterConfig<FoodMeasure, FoodMeasureResponse>
                .NewConfig();
        }
    }
}
