using MediatR;

namespace NOVIX.Identity.Application.Auth.Commands.Login
{
    public class LoginCommand : IRequest<LoginResponseDto>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
