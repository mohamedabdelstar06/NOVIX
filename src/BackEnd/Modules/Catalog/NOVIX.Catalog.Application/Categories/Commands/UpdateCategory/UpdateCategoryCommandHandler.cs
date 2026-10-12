using MediatR;
using NOVIX.Catalog.Domain.Entities;
using NOVIX.Shared.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Category, Guid> _categoryRepository;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<Category, Guid> categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null) return false;

            category.Name = request.Name;
            category.Description = request.Description;
            category.ParentCategoryId = request.ParentCategoryId;

            _categoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
