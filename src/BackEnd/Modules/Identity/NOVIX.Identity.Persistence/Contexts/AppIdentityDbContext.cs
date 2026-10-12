using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NOVIX.Identity.Domain.Entities;
using System;

namespace NOVIX.Identity.Persistence.Contexts
{
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("identity");

            // Rename ASP.NET Identity tables
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<ApplicationRole>().ToTable("Roles");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserToken<Guid>>().ToTable("UserTokens");
        }
    }
}
