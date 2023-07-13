using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Application.Contracts
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile formFile);
        byte[]? GetFile(string fileName);
    }
}
