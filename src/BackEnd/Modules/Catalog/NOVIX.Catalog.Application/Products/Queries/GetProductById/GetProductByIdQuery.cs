using MediatR;
using NOVIX.Catalog.Application.Products.Queries.GetProductsWithPagination;
using System;

namespace NOVIX.Catalog.Application.Products.Queries.GetProductById
{
    public class ProductDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public bool IsPublished { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
    }

    public class GetProductByIdQuery : IRequest<ProductDetailsDto>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
