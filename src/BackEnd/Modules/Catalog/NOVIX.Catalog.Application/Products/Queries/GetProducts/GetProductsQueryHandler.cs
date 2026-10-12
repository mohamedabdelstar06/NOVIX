using MediatR;
using Microsoft.EntityFrameworkCore;
using NOVIX.Catalog.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Application.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
    {
        private readonly ICatalogDbContext _context;

        public GetProductsQueryHandler(ICatalogDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Slug = p.Slug,
                    BasePrice = p.BasePrice,
                    CategoryName = p.Category != null ? p.Category.Name : string.Empty,
                    BrandName = p.Brand != null ? p.Brand.Name : string.Empty
                })
                .ToListAsync(cancellationToken);
        }
    }
}
