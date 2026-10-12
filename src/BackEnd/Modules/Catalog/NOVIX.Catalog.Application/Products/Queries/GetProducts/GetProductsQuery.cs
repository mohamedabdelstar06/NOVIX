using MediatR;
using System.Collections.Generic;

namespace NOVIX.Catalog.Application.Products.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<List<ProductDto>>
    {
    }
}
