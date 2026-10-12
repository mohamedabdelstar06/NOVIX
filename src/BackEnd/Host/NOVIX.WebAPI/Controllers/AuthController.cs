using MediatR;
using Microsoft.AspNetCore.Mvc;
using NOVIX.Identity.Application.Auth.Commands.GoogleLogin;
using NOVIX.Identity.Application.Auth.Commands.Login;
using NOVIX.Identity.Application.Auth.Commands.Register;
using System.Threading.Tasks;

namespace NOVIX.WebAPI.Controllers
{
    [Route("api/identity/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(new { Message = result.ErrorMessage });

            return Ok(new { Message = "User created successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return Unauthorized(new { Message = result.ErrorMessage });

            return Ok(new { Token = result.Token });
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return BadRequest(new { Message = result.ErrorMessage });

            return Ok(new { Token = result.Token });
        }
    }
}
