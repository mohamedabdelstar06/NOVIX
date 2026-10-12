using Microsoft.EntityFrameworkCore;
using NOVIX.Shared.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NOVIX.Catalog.Persistence.Contexts;

namespace NOVIX.Catalog.Persistence.Repositories
{
    public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId> where TEntity : class
    {
        protected readonly CatalogDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(CatalogDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(TId id, System.Threading.CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> AddAsync(TEntity entity, System.Threading.CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
