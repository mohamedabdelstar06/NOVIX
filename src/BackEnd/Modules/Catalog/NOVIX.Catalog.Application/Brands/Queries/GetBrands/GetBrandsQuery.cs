using MediatR;
using Microsoft.EntityFrameworkCore;
using NOVIX.Catalog.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Application.Brands.Queries.GetBrands
{
    public class BrandDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
    }

    public class GetBrandsQuery : IRequest<List<BrandDto>> { }

    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, List<BrandDto>>
    {
        private readonly ICatalogDbContext _context;

        public GetBrandsQueryHandler(ICatalogDbContext context)
        {
            _context = context;
        }

        public async Task<List<BrandDto>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Brands
                .Select(b => new BrandDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Description = b.Description,
                    LogoUrl = b.LogoUrl
                }).ToListAsync(cancellationToken);
        }
    }
}
