namespace RealEstate.Application.Contracts
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? UserName { get; }
        bool IsInRole(string role);
    }
}
