using CalorieTracker.DAL.Utils;
using CalorieTracker.BLL.Service;
using Microsoft.AspNetCore.Identity.UI.Services;
using CalorieTracker.DAL.Repositories.Interfaces;
using CalorieTracker.BLL.Interfaces;
using CalorieTracker.DAL.Repository.Implementations;

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
            Services.AddExceptionHandler<GlobalExceptionHandler>();
            Services.AddProblemDetails();
        }
    }
}
