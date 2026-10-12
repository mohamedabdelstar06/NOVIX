using MediatR;
using NOVIX.Identity.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NOVIX.Identity.Application.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponseDto>
    {
        private readonly IIdentityService _identityService;

        public RegisterCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<RegisterResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.RegisterAsync(request.Email, request.Password, request.FirstName, request.LastName);
            return new RegisterResponseDto
            {
                IsSuccess = result.IsSuccess,
                ErrorMessage = result.ErrorMessage
            };
        }
    }
}
