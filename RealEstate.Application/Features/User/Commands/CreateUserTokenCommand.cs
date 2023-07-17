using FluentValidation;
using MediatR;
using RealEstate.Application.Contracts;
using System.Security.Authentication;

namespace RealEstate.Application.Features.User.Commands
{
    public record CreateUserTokenCommandRequest : IRequest<string>
    {
        public string? UserName { get; init; }
        public string? Password { get; init; }
    }

    public class CreateUserTokenCommandValidation : AbstractValidator<CreateUserTokenCommandRequest>
    {
        public CreateUserTokenCommandValidation()
        {
            RuleFor(r => r.UserName).NotEmpty().EmailAddress();
            RuleFor(r => r.Password).NotEmpty();
        }
    }

    public class CreateUserTokenCommandHanlder : IRequestHandler<CreateUserTokenCommandRequest, string>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IIdentityService _identityService;

        public CreateUserTokenCommandHanlder(IJwtProvider jwtProvider, IIdentityService identityService)
        {
            _jwtProvider = jwtProvider;
            _identityService = identityService;
        }

        public async Task<string> Handle(CreateUserTokenCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _identityService.CheckPasswordAsync(request.UserName, request.Password);
            if (!result)
            {
                throw new AuthenticationException("UserName or Password not valid");
            }
            return await _jwtProvider.GenerateAsync(request.UserName);
        }
    }
}
