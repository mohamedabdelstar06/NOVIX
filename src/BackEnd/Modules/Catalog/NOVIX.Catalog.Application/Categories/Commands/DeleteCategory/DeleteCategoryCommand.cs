using MediatR;
using System;

namespace NOVIX.Catalog.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteCategoryCommand(Guid id) => Id = id;
    }
}
