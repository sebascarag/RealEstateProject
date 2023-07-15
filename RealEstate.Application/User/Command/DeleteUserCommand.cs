using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using RealEstate.Application.Contracts;
using RealEstate.Domain.Constants;

namespace RealEstate.Application.User.Command
{
    [Authorize(Roles = Roles.Administrator)]
    public record DeleteUserCommandRequest(string UserName) : IRequest<bool> { }

    public class DeleteUserCommandValidation : AbstractValidator<DeleteUserCommandRequest>
    {
        public DeleteUserCommandValidation()
        {
            RuleFor(r => r.UserName).NotEmpty().EmailAddress();
        }
    }

    public class DeleteUserCommandHanlder : IRequestHandler<DeleteUserCommandRequest, bool>
    {
        private readonly IIdentityService _identityService;

        public DeleteUserCommandHanlder(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<bool> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
            => await _identityService.DeleteUserAsync(request.UserName);
    }
}
