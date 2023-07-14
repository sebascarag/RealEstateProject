namespace RealEstate.Application.Contracts
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        string? UserName { get; }
        bool IsInRoleAsync(string role);
    }
}
