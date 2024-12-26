using Microsoft.AspNetCore.Http;
using Mirror.Domain.Entities;

namespace Mirror.Application.Services.FileService
{
    public interface IFileService
    {
        public Task SaveFileToBlob(IFormFile file);

        public Task<Image> GetFileFromBlobByUserId(Image image, Guid userId);

        public Task<List<Image>> GetFilesFromBlob(Image image);
    }
}
