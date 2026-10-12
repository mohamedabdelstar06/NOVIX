using MediatR;
using Microsoft.EntityFrameworkCore;
using NOVIX.Catalog.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Application.Categories.Queries.GetCategories
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class GetCategoriesQuery : IRequest<List<CategoryDto>> { }

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
    {
        private readonly ICatalogDbContext _context;

        public GetCategoriesQueryHandler(ICatalogDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                }).ToListAsync(cancellationToken);
        }
    }
}
