using CalorieTracker.DAL.Utils;
using CalorieTracker.BLL.Service;
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
            Services.AddExceptionHandler<GlobalExceptionHandler>();
            Services.AddProblemDetails();
        }
    }
}
