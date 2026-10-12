using MediatR;
using Microsoft.EntityFrameworkCore;
using NOVIX.Catalog.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly ICatalogDbContext _context;

        public DeleteProductCommandHandler(ICatalogDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
