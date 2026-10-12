using MediatR;
using NOVIX.Catalog.Domain.Entities;
using NOVIX.Shared.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Category, Guid> _categoryRepository;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<Category, Guid> categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null) return false;

            _categoryRepository.Delete(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
