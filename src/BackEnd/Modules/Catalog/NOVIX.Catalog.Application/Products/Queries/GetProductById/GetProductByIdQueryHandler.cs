using MediatR;
using Microsoft.EntityFrameworkCore;
using NOVIX.Catalog.Application.Interfaces;
using NOVIX.Catalog.Application.Products.Queries.GetProductsWithPagination;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDetailsDto>
    {
        private readonly ICatalogDbContext _context;

        public GetProductByIdQueryHandler(ICatalogDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDetailsDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Where(p => p.Id == request.Id)
                .Select(p => new ProductDetailsDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Slug = p.Slug,
                    ShortDescription = p.ShortDescription,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    BasePrice = p.BasePrice,
                    IsPublished = p.IsPublished,
                    CategoryId = p.CategoryId,
                    BrandId = p.BrandId
                })
                .FirstOrDefaultAsync(cancellationToken);

            return product!;
        }
    }
}
