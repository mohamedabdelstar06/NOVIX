using MediatR;
using System;

namespace NOVIX.Catalog.Application.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        
        public Microsoft.AspNetCore.Http.IFormFile? LogoFile { get; set; }
    }
}
