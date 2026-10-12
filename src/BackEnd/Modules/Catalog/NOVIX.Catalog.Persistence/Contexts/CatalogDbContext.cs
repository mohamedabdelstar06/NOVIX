using Microsoft.EntityFrameworkCore;
using NOVIX.Catalog.Domain.Entities;
using System.Reflection;
using NOVIX.Catalog.Application.Interfaces;

namespace NOVIX.Catalog.Persistence.Contexts
{
    public class CatalogDbContext : DbContext, ICatalogDbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Ignore<NOVIX.Shared.Core.Domain.DomainEvent>();

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
