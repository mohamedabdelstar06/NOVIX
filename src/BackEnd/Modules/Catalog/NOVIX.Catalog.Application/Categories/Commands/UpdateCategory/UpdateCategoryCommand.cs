using MediatR;
using System;

namespace NOVIX.Catalog.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
    }
}
