using MediatR;
using Microsoft.EntityFrameworkCore;
using NOVIX.Catalog.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly ICatalogDbContext _context;
        private readonly IFileStorageService _fileStorageService;

        public UpdateProductCommandHandler(ICatalogDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (product == null) return false;

            var imageUrl = request.ImageUrl;
            if (request.ImageFile != null)
            {
                imageUrl = await _fileStorageService.SaveFileAsync(request.ImageFile.OpenReadStream(), request.ImageFile.FileName);
            }

            product.Update(
                request.Name,
                request.Slug,
                request.ShortDescription,
                request.Description,
                imageUrl,
                request.BasePrice,
                request.CategoryId,
                request.BrandId,
                request.IsPublished
            );

            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
