using MediatR;

namespace NOVIX.Identity.Application.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<RegisterResponseDto>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }

    public class RegisterResponseDto
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
