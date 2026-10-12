using MediatR;
using System;

namespace NOVIX.Catalog.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public bool IsPublished { get; set; }
        
        public Microsoft.AspNetCore.Http.IFormFile? ImageFile { get; set; }
    }
}
