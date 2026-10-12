using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NOVIX.Catalog.Persistence.Contexts;

namespace NOVIX.Catalog.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCatalogPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(CatalogDbContext).Assembly.FullName)));

            services.AddScoped(typeof(NOVIX.Shared.Core.Interfaces.IGenericRepository<,>), typeof(NOVIX.Catalog.Persistence.Repositories.GenericRepository<,>));
            services.AddScoped<NOVIX.Shared.Core.Interfaces.IUnitOfWork, NOVIX.Catalog.Persistence.Repositories.UnitOfWork>();
            services.AddScoped<NOVIX.Catalog.Application.Interfaces.ICatalogDbContext>(provider => provider.GetRequiredService<CatalogDbContext>());

            return services;
        }
    }
}
