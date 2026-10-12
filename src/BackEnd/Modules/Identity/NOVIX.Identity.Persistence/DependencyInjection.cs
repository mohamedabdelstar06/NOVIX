using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NOVIX.Identity.Domain.Entities;
using NOVIX.Identity.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;

namespace NOVIX.Identity.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppIdentityDbContext).Assembly.FullName)));

            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>();

            return services;
        }
    }
}
