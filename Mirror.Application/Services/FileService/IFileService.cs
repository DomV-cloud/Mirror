using Microsoft.AspNetCore.Http;
using Mirror.Domain.Entities;

namespace Mirror.Application.Services.FileService
{
    public interface IFileService
    {
        string SaveFileToBlob(IFormFile file);
        Task DeleteFile(string filePath);
    }
}
