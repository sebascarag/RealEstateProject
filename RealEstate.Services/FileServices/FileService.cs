using Microsoft.AspNetCore.Http;
using RealEstate.Application.Contracts;

namespace RealEstate.Services.FileServices
{
    public class FileService : IFileService
    {
        private readonly string _contentRootPath;

        public FileService()
        {
            _contentRootPath = Environment.CurrentDirectory;
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            var path = Path.Combine(_contentRootPath, "Files", fileName);
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            return true;
        }

        public async Task<byte[]?> GetFileAsync(string fileName)
        {
            var path = Path.Combine(_contentRootPath, "Files", fileName);
            if (File.Exists(path))
            {
                return await File.ReadAllBytesAsync(path);
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
                    return fileName;
                }
                else
                {
                    throw new Exception("File without content");
                }
            }
            catch (Exception)
            {
                throw new Exception("File could not be save");
            }
        }
    }
}
