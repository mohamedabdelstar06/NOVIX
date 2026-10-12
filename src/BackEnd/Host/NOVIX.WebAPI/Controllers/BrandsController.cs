using MediatR;
using Microsoft.AspNetCore.Mvc;
using NOVIX.Catalog.Application.Brands.Queries.GetBrands;
using NOVIX.Catalog.Application.Brands.Commands.CreateBrand;
using NOVIX.Catalog.Application.Brands.Commands.UpdateBrand;
using NOVIX.Catalog.Application.Brands.Commands.DeleteBrand;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NOVIX.WebAPI.Controllers
{
    [Route("api/catalog/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var result = await _mediator.Send(new GetBrandsQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromForm] CreateBrandCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBrand(Guid id, [FromForm] UpdateBrandCommand command)
        {
            if (id != command.Id) return BadRequest();

            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            var result = await _mediator.Send(new DeleteBrandCommand(id));
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
