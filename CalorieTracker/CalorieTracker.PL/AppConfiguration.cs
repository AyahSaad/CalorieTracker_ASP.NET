using CalorieTracker.BLL.Interfaces;
using CalorieTracker.BLL.Service;
using CalorieTracker.DAL.Repositories.Interfaces;
using CalorieTracker.DAL.Repository.Implementations;
using CalorieTracker.DAL.Utils;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CalorieTracker.PL
{
    public static class AppConfiguration
    {
        public static void Config(IServiceCollection Services)
        {
            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<ISeedData, RoleSeedData>();
            Services.AddScoped<ISeedData, UserSeedData>();
            Services.AddTransient<IEmailSender, EmailSender>();
            Services.AddScoped<ITokenService, TokenService>();
            Services.AddScoped<IManageUserService, ManageUserService>();
            Services.AddScoped<IFoodRepository, FoodRepository>();
            Services.AddHttpClient<IEdamamService, EdamamService>();
            Services.AddScoped<IFoodService, FoodService>();
            Services.AddScoped<IMealRepository, MealRepository>();
            Services.AddScoped<IMealService, MealService>();
            Services.AddScoped<IReportService, ReportService>();
            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<IWeightLogRepository, WeightLogRepository>();
            Services.AddScoped<IWeightLogService, WeightLogService>();
            Services.AddScoped<IFavoriteFoodRepository, FavoriteFoodRepository>();
            Services.AddScoped<IFavoriteFoodService, FavoriteFoodService>();
            Services.AddExceptionHandler<GlobalExceptionHandler>();
            Services.AddProblemDetails();
        }
    }
}
