using MediatR;
using System;

namespace NOVIX.Catalog.Application.Brands.Commands.DeleteBrand
{
    public class DeleteBrandCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteBrandCommand(Guid id) => Id = id;
    }
}
