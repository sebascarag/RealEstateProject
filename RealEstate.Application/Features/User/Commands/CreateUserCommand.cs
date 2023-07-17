using FluentValidation;
using MediatR;
using RealEstate.Application.Contracts;
using RealEstate.Application.Exceptions;

namespace RealEstate.Application.Features.User.Commands
{
    public record CreateUserCommandRequest : IRequest<bool>
    {
        public string? UserName { get; init; }
        public string? Password { get; init; }
    }

    public class CreateUserCommandValidation : AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserCommandValidation()
        {
            RuleFor(r => r.UserName).NotEmpty().EmailAddress();
            RuleFor(r => r.Password).NotEmpty();
        }
    }

    public class CreateUserCommandHanlder : IRequestHandler<CreateUserCommandRequest, bool>
    {
        private readonly IIdentityService _identityService;

        public CreateUserCommandHanlder(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<bool> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var (result, errors) = await _identityService.CreateUserAsync(request.UserName, request.Password);
            if (!result)
                throw new ApiException(string.Join(", ", errors));
            return result;
        }
    }
}
