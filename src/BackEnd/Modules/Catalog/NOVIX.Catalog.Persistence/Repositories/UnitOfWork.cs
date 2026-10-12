using NOVIX.Shared.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using NOVIX.Catalog.Persistence.Contexts;

namespace NOVIX.Catalog.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogDbContext _context;
        private bool _disposed;

        public UnitOfWork(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
