using MediatR;
using NOVIX.Catalog.Domain.Entities;
using NOVIX.Shared.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Application.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Brand, Guid> _brandRepository;
        private readonly NOVIX.Catalog.Application.Interfaces.IFileStorageService _fileStorageService;

        public UpdateBrandCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<Brand, Guid> brandRepository, NOVIX.Catalog.Application.Interfaces.IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _brandRepository = brandRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<bool> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetByIdAsync(request.Id);
            if (brand == null) return false;

            var logoUrl = request.LogoUrl;
            if (request.LogoFile != null)
            {
                logoUrl = await _fileStorageService.SaveFileAsync(request.LogoFile.OpenReadStream(), request.LogoFile.FileName);
            }

            brand.Name = request.Name;
            brand.Description = request.Description;
            brand.LogoUrl = logoUrl;

            _brandRepository.Update(brand);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
