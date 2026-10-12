using MediatR;
using NOVIX.Identity.Application.Auth.Commands.Login;
using NOVIX.Identity.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Identity.Application.Auth.Commands.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, LoginResponseDto>
    {
        private readonly IIdentityService _identityService;

        public GoogleLoginCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<LoginResponseDto> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.GoogleLoginAsync(request.IdToken);
            return new LoginResponseDto
            {
                IsSuccess = result.IsSuccess,
                Token = result.Token,
                ErrorMessage = result.ErrorMessage
            };
        }
    }
}
