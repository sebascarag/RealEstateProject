using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using RealEstate.Application.Contracts;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Constants;

namespace RealEstate.Application.Features.User.Command
{
    [Authorize]
    public record AddUserAdminRoleCommandRequest(string UserName) : IRequest<bool> { }

    public class AddUserAdminRoleCommandValidation : AbstractValidator<AddUserAdminRoleCommandRequest>
    {
        public AddUserAdminRoleCommandValidation()
        {
            RuleFor(r => r.UserName).NotEmpty().EmailAddress();
        }
    }

    public class AddUserAdminRoleCommandHanlder : IRequestHandler<AddUserAdminRoleCommandRequest, bool>
    {
        private readonly IIdentityService _identityService;

        public AddUserAdminRoleCommandHanlder(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<bool> Handle(AddUserAdminRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var (result, errors) = await _identityService.AddUserToRoleAync(request.UserName, Roles.Administrator);
            if (!result)
                throw new ApiException(string.Join(", ", errors));
            return result;
        }
    }
}
