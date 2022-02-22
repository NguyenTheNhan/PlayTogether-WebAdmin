using Microsoft.Extensions.DependencyInjection;
using WebAdmin.Client.Services.Interfaces;

namespace WebAdmin.Client.Services
{
    public static class DenpendencyInjectionExtensions
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            return services.AddScoped<IAuthenticationService, HttpAuthenticationService>()
                           .AddScoped<IGameTypeService, HttpGameTypeService>();
        }
    }
}
