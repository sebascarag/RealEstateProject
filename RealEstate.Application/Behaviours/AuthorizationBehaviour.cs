using MediatR;
using Microsoft.AspNetCore.Authorization;
using RealEstate.Application.Contracts;
using System.Reflection;

namespace RealEstate.Application.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ICurrentUserService _user;

        public AuthorizationBehaviour(ICurrentUserService user)
        {
            _user = user;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                // Must be authenticated user
                if (_user.UserId == null)
                {
                    throw new UnauthorizedAccessException();
                }

                // Role-based authorization
                var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

                if (authorizeAttributesWithRoles.Any())
                {
                    var authorized = false;

                    foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles?.Split(',')))
                    {
                        foreach (var role in roles)
                        {
                            // Check authorize roles in user context
                            var isInRole = _user.IsInRole(role);
                            if (isInRole)
                            {
                                authorized = true;
                                break;
                            }
                        }
                    }

                    // Must be a member of at least one role in roles
                    if (!authorized)
                    {
                        throw new UnauthorizedAccessException();
                    }
                }
            }

            // User is authorized / authorization not required
            return await next();
        }
    }
}
