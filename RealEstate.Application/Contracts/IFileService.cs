using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Application.Contracts
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile formFile);
        Task<bool> DeleteFileAsync(string fileName);
        Task<byte[]?> GetFileAsync(string fileName);
    }
}
