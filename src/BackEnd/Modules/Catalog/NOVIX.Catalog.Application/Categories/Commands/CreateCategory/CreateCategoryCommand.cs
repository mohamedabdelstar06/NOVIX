using MediatR;
using System;

namespace NOVIX.Catalog.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
    }
}
