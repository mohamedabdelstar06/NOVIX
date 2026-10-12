using Microsoft.Extensions.DependencyInjection;
using NOVIX.Catalog.Application.Interfaces;
using NOVIX.Catalog.Infrastructure.Services;

namespace NOVIX.Catalog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IFileStorageService, FileStorageService>();
            return services;
        }
    }
}
