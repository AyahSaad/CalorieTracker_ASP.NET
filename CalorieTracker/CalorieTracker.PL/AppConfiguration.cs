using Microsoft.AspNetCore.Identity.UI.Services;

namespace CalorieTracker.PL
{
    public static class AppConfiguration
    {
        public static void Config(IServiceCollection Services)
        {
           
            Services.AddProblemDetails();
        }
    }

}
