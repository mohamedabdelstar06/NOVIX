using MediatR;
using NOVIX.Identity.Application.Auth.Commands.Login;

namespace NOVIX.Identity.Application.Auth.Commands.GoogleLogin
{
    public class GoogleLoginCommand : IRequest<LoginResponseDto>
    {
        public string IdToken { get; set; } = string.Empty;
    }
}
