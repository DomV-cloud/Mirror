using Microsoft.AspNetCore.Http;
using Mirror.Application.DatabaseContext;
using Mirror.Domain.Entities;

namespace Mirror.Application.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly MirrorContext _context;

        public FileService(MirrorContext context)
        {
            _context = context;
        }

        public Task<Image> GetFileFromBlobByUserId(Image image, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Image>> GetFilesFromBlob(Image image)
        {
            throw new NotImplementedException();
        }

        public async Task SaveFileToBlob(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var imageData = ms.ToArray();

            var image = new Image
            {
                Name = file.FileName,
                Data = imageData,
                ContentType = file.ContentType
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();
        }
    }
}
