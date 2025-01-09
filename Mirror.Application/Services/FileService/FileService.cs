using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mirror.Application.DatabaseContext;
using Mirror.Domain.Entities;

namespace Mirror.Application.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly MirrorContext _context;
        private readonly ILogger<FileService> _logger;

        public FileService(MirrorContext context, ILogger<FileService> logger)
        {
            _context = context;
            _logger = logger;
        }

        ///TODO: I would consider to split FileService to ImageService
        public async Task DeleteMultipleFiles(List<Guid> imagesIds)
        {
            foreach (var image in imagesIds)
            {
                var imageToDelete = await _context.Images.FirstOrDefaultAsync(i => i.Id == image);

                if (imageToDelete is not null)
                {
                    _context.Images.Remove(imageToDelete);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Image> SaveFileToBlob(IFormFile file)
        {
            _logger.LogInformation("Saving file {FileName} to blob storage", file.FileName);

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var imageData = ms.ToArray();

            var image = new Image
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                Content = ms.ToArray()
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            _logger.LogInformation("File {FileName} saved successfully to blob storage with ID {ImageId}", file.FileName, image.Id);

            return image;
        }

        public async Task<int> SaveMultipleFilesToBlob(List<IFormFile> files)
        {
            List<IFormFile> newlyAddedFiles = [];

            foreach (var file in files)
            {
                await SaveFileToBlob(file);
                newlyAddedFiles.Add(file);
            }

            return newlyAddedFiles.Count; //temp
        }
    }
}