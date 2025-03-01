﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mirror.Application.DatabaseContext;
using Mirror.Application.Services.FileService;
using Mirror.Application.Services.Repository.Image;
using System.IO;

namespace Mirror.Infrastructure.Persistance.Repository.Image
{
    public class ImageRepository : IImageRepository
    {
        private readonly ILogger<ImageRepository> _logger;
        private readonly MirrorContext _context;
        private readonly IFileService _fileService;

        public ImageRepository(ILogger<ImageRepository> logger, MirrorContext context, IFileService fileService)
        {
            _logger = logger;
            _context = context;
            _fileService = fileService;
        }

        public async Task<Domain.Entities.Image> UploadImageAsync(IFormFile file)
        {
            _logger.LogInformation("Saving image {FileName}", file.FileName);

            var filePath =  _fileService.SaveFileToBlob(file);

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var imageContent = ms.ToArray();

            var imageToSave = new Domain.Entities.Image
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                FilePath = filePath,
                Content = imageContent
            };

            _context.Images.Add(imageToSave);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Image {FileName} saved successfully with ID {ImageId}", file.FileName, imageToSave.Id);

            return imageToSave;
        }

        public async Task DeleteImageAsync(List<Guid> imageIdsToDelete)
        {
            if (imageIdsToDelete.Count == 0 || imageIdsToDelete is null)
            {
                return;
            }

            foreach (var imageId in imageIdsToDelete)
            {
                if (imageId != Guid.Empty)
                {
                    var imageToDelete = await _context.Images.FirstOrDefaultAsync(i => i.Id == imageId);
                    if (imageToDelete == null)
                    {
                        return;
                    }

                    _context.Images.Remove(imageToDelete);
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
