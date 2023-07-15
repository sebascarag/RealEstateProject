using System.Security.Claims;

namespace RealEstate.Application.Contracts
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(string userId);
        Task<bool> CheckPasswordAsync(string userName, string password);
        Task<(bool result, IList<string> errors)> AddUserToRoleAync(string userName, string role);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<IList<Claim>> GetUserClaimsAsync(string userName);
        Task<(bool result, IList<string> errors)> CreateUserAsync(string userName, string password);
        Task<bool> DeleteUserAsync(string userName);
    }
}
