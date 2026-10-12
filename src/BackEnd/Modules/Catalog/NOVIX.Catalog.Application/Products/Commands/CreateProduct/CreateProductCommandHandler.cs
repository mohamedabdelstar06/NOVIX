using MediatR;
using NOVIX.Catalog.Application.Interfaces;
using NOVIX.Catalog.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly ICatalogDbContext _context;
        private readonly IFileStorageService _fileStorageService;

        public CreateProductCommandHandler(ICatalogDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var imageUrl = request.ImageUrl;
            if (request.ImageFile != null)
            {
                imageUrl = await _fileStorageService.SaveFileAsync(request.ImageFile.OpenReadStream(), request.ImageFile.FileName);
            }

            var product = Product.Create(
                request.Name,
                request.Slug,
                request.ShortDescription,
                request.Description,
                imageUrl,
                request.BasePrice,
                request.CategoryId,
                request.BrandId
            );

            if (request.IsPublished)
            {
                product.Publish();
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
