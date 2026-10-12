using MediatR;
using NOVIX.Catalog.Domain.Entities;
using NOVIX.Shared.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Application.Brands.Commands.CreateBrand
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Brand, Guid> _brandRepository;
        private readonly NOVIX.Catalog.Application.Interfaces.IFileStorageService _fileStorageService;

        public CreateBrandCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<Brand, Guid> brandRepository, NOVIX.Catalog.Application.Interfaces.IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _brandRepository = brandRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<Guid> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var logoUrl = request.LogoUrl;
            if (request.LogoFile != null)
            {
                logoUrl = await _fileStorageService.SaveFileAsync(request.LogoFile.OpenReadStream(), request.LogoFile.FileName);
            }

            var brand = new Brand
            {
                Name = request.Name,
                Description = request.Description,
                LogoUrl = logoUrl
            };

            await _brandRepository.AddAsync(brand);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return brand.Id;
        }
    }
}
