using Microsoft.Extensions.DependencyInjection;
using PTO_WebAdmin.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTO_WebAdmin.Client.Services
{
    public static class DenpendencyInjectionExtensions
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            return services.AddScoped<IAuthenticationService, HttpAuthenticationService>();
        }
        }
}
