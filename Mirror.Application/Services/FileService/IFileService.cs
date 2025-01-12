using Microsoft.AspNetCore.Http;
using Mirror.Domain.Entities;

namespace Mirror.Application.Services.FileService
{
    public interface IFileService
    {
        Task<string> SaveFileToBlob(IFormFile file);
        Task DeleteFile(string filePath);
    }
}
