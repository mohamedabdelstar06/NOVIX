using MediatR;
using NOVIX.Shared.Core.Models;
using System.Collections.Generic;

namespace NOVIX.Catalog.Application.Products.Queries.GetProductsWithPagination
{
    public class GetProductsWithPaginationQuery : IRequest<PagedResult<ProductDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SearchTerm { get; set; } = string.Empty;
    }

    public class ProductDto
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public bool IsPublished { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
    }
}
