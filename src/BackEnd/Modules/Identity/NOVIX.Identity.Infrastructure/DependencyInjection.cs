using Microsoft.Extensions.DependencyInjection;
using NOVIX.Identity.Application.Interfaces;
using NOVIX.Identity.Infrastructure.Services;

namespace NOVIX.Identity.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            return services;
        }
    }
}
