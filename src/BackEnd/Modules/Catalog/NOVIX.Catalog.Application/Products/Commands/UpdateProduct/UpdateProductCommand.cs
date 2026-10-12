using MediatR;
using System;
using Microsoft.AspNetCore.Http;

namespace NOVIX.Catalog.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public bool IsPublished { get; set; }
        
        public IFormFile? ImageFile { get; set; }
    }
}
