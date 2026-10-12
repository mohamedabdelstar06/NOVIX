using System;

namespace NOVIX.Catalog.Application.Products.Queries.GetProducts
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
    }
}
