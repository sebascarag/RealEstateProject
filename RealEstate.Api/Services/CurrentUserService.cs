using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Contracts;
using System.Security.Claims;

namespace RealEstate.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly bool _hasClaims;
        private readonly string _userId;
        private readonly string _userName;
        private readonly string? _roles;
        private readonly HttpContext? _httpContext;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
            // check if context has any claim
            _hasClaims = _httpContext != null && _httpContext?.User.Identity is ClaimsIdentity claims && claims.Claims.Any();
            if (_hasClaims)
            {
                _userId = _httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _userName = _httpContext.User.FindFirstValue(ClaimTypes.Email);
                _roles = _httpContext.User.FindFirstValue(ClaimTypes.Role);
            }
        }

        public string? UserId { get => _hasClaims ? _userId : null; } // return userId if context has claims
        public string? UserName { get => _hasClaims ? _userName : null; } // return userName if context has claims
        public bool IsInRole(string role)
        {
            if (_roles == null) 
                return false;

            // Split the token roles string into an array
            var userRoles = _roles.Split(',');
            var checkRoles = role.Split(',');

            // Succeed if the claimRoles array contains the required role
            return userRoles.Any(ur => checkRoles.Contains(ur));
        }
    }
}
