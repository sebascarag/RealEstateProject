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
        private readonly HttpContext? _httpContext;
        public CurrentUserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext.HttpContext;
            // check if context has any claim
            _hasClaims = _httpContext != null && _httpContext?.User.Identity is ClaimsIdentity claims && claims.Claims.Any();
            if (_hasClaims)
            {
                _userId = _httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _userName = _httpContext.User.FindFirstValue(ClaimTypes.Email);
            }
        }

        public int? UserId { get => _hasClaims ? int.Parse(_userId) : null; } // return userId if context has claims
        public string? UserName { get => _hasClaims ? _userName : null; } // return userName if context has claims
        public bool IsInRoleAsync(string role)
        {
            // Check if context has role claim
            if (_httpContext != null && !_httpContext.User.HasClaim(c => c.Type == ClaimsIdentity.DefaultRoleClaimType))
                return false;

            // Split the token roles string into an array
            var claimRoles = _httpContext?.User.FindFirst(c => c.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value.Split(',');

            // Succeed if the claimRoles array contains the required role
            return claimRoles != null && claimRoles.Any(r => role.Split(',').Contains(r));
        }
    }
}
