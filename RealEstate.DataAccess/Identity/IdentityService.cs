using Microsoft.AspNetCore.Identity;
using RealEstate.Application.Contracts;
using System.Security.Authentication;
using System.Security.Claims;

namespace RealEstate.DataAccess.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

        public IdentityService(
            UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        public async Task<bool> CheckPasswordAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user.UserName;
        }

        public async Task<(bool result, IList<string> errors)> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToList());
        }

        public async Task<(bool result, IList<string> errors)> AddUserToRoleAync(string userName, string role)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return (false, new List<string> { "UserName not found" }); ;
            }
            var result = await _userManager.AddToRoleAsync(user, role);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToList());
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<IList<Claim>> GetUserClaimsAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName)
                ?? throw new AuthenticationException();

            // get principal user claims
            var claims = (await _userClaimsPrincipalFactory.CreateAsync(user)).Claims.ToList();

            return claims;
        }

        public async Task<bool> DeleteUserAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded;
            }
            return true;
        }
    }
}
