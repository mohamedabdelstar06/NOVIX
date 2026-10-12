using MediatR;
using Microsoft.EntityFrameworkCore;
using NOVIX.Catalog.Application.Interfaces;
using NOVIX.Shared.Core.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Application.Products.Queries.GetProductsWithPagination
{
    public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PagedResult<ProductDto>>
    {
        private readonly ICatalogDbContext _context;

        public GetProductsWithPaginationQueryHandler(ICatalogDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ProductDto>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(p => p.Name.Contains(request.SearchTerm) || p.Category.Name.Contains(request.SearchTerm));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(p => p.Name)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Slug = p.Slug,
                    BasePrice = p.BasePrice,
                    IsPublished = p.IsPublished,
                    CategoryName = p.Category.Name,
                    BrandName = p.Brand.Name
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<ProductDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
