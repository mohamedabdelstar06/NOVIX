using MediatR;
using Microsoft.AspNetCore.Mvc;
using NOVIX.Catalog.Application.Categories.Queries.GetCategories;
using NOVIX.Catalog.Application.Categories.Commands.CreateCategory;
using NOVIX.Catalog.Application.Categories.Commands.UpdateCategory;
using NOVIX.Catalog.Application.Categories.Commands.DeleteCategory;
using System;
using System.Threading.Tasks;

namespace NOVIX.WebAPI.Controllers
{
    [Route("api/catalog/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _mediator.Send(new GetCategoriesQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryCommand command)
        {
            if (id != command.Id) return BadRequest();
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id));
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
