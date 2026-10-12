using MediatR;
using Microsoft.AspNetCore.Mvc;
using NOVIX.Catalog.Application.Products.Commands.CreateProduct;
using NOVIX.Catalog.Application.Products.Commands.UpdateProduct;
using NOVIX.Catalog.Application.Products.Commands.DeleteProduct;
using NOVIX.Catalog.Application.Products.Queries.GetProductById;
using NOVIX.Catalog.Application.Products.Queries.GetProductsWithPagination;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NOVIX.WebAPI.Controllers
{
    [Route("api/catalog/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsWithPaginationQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProductById), new { id = result }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromForm] UpdateProductCommand command)
        {
            if (id != command.Id) return BadRequest();

            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
