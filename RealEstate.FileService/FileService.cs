using Microsoft.AspNetCore.Http;
using RealEstate.Application.Contracts;

namespace RealEstate.FileService
{
    public class FileService : IFileService
    {
        private readonly string _contentRootPath;

        public FileService()
        {
            _contentRootPath = Environment.CurrentDirectory;
        }

        public byte[]? GetFile(string fileName)
        {
            var path = Path.Combine(_contentRootPath, "Files", fileName);
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            return null;
        }

        public async Task<string> SaveFileAsync(IFormFile formFile)
        {
            try
            {
                string fileName = "";
                string uploads = Path.Combine(_contentRootPath, "Files");
                if (formFile.Length > 0)
                {
                    fileName = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";

                    string filePath = Path.Combine(uploads, fileName);
                    using Stream fileStream = new FileStream(filePath, FileMode.Create);
                    await formFile.CopyToAsync(fileStream);
                }
                return fileName;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}