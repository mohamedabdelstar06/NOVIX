using MediatR;
using NOVIX.Catalog.Domain.Entities;
using NOVIX.Shared.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Category, Guid> _categoryRepository;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<Category, Guid> categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                ParentCategoryId = request.ParentCategoryId
            };

            await _categoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}
