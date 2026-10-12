using Microsoft.EntityFrameworkCore;
using NOVIX.Catalog.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Application.Interfaces
{
    public interface ICatalogDbContext
    {
        DbSet<Category> Categories { get; }
        DbSet<Brand> Brands { get; }
        DbSet<Product> Products { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
