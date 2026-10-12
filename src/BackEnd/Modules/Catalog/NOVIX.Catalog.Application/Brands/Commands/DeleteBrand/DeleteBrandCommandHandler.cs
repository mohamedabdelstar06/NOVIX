using MediatR;
using NOVIX.Catalog.Domain.Entities;
using NOVIX.Shared.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Application.Brands.Commands.DeleteBrand
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Brand, Guid> _brandRepository;

        public DeleteBrandCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<Brand, Guid> brandRepository)
        {
            _unitOfWork = unitOfWork;
            _brandRepository = brandRepository;
        }

        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetByIdAsync(request.Id);
            if (brand == null) return false;

            _brandRepository.Delete(brand);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
