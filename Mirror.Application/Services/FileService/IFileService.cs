using Microsoft.AspNetCore.Http;
using Mirror.Domain.Entities;

namespace Mirror.Application.Services.FileService
{
    public interface IFileService
    {
        public Task<Image> SaveFileToBlob(IFormFile file);

        public Task<string> SaveFileToDisk(IFormFile file, Guid memoryId);

        public Task<string?> ValidateAndSaveFile(IFormFile file, Guid memoryId);
    }
}
