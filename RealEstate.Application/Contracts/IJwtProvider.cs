namespace RealEstate.Application.Contracts
{
    public interface IJwtProvider
    {
        Task<string> GenerateAsync(string userName);
    }
}
