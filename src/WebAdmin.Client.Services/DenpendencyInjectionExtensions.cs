using Microsoft.Extensions.DependencyInjection;
using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Client.Services.Services;

namespace WebAdmin.Client.Services
{
    public static class DenpendencyInjectionExtensions
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            return services.AddScoped<IAuthenticationService, HttpAuthenticationService>()
                           .AddScoped<IGameTypeService, HttpGameTypeService>()
                           .AddScoped<IGameService, HttpGameService>()
                           .AddScoped<ITypeOfGameService, HttpTypeOfGameService>()
                           .AddScoped<IRankService, HttpRankService>()
                           .AddScoped<IHirerService, HttpHirerService>()
                           .AddScoped<IOrderService, HttpOrderService>()
                           .AddScoped<ITransactionService, HttpTransactionService>()
                           .AddScoped<ICharitiesService, HttpCharitiesService>()
                           .AddScoped<IReportService, HttpReportService>();
        }
    }
}
