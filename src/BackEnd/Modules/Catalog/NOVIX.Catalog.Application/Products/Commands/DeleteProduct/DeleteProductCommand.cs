using MediatR;
using System;

namespace NOVIX.Catalog.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
