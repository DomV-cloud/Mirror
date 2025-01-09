using Microsoft.AspNetCore.Http;
using Mirror.Domain.Entities;

namespace Mirror.Application.Services.FileService
{
    public interface IFileService
    {
        public Task<Image> SaveFileToBlob(IFormFile file);

        public Task DeleteMultipleFiles(List<Guid> imagesIds);

        public Task<int> SaveMultipleFilesToBlob(List<IFormFile> files);
    }
}
