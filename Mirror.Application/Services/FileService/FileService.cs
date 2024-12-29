using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ILogger<FileService> _logger;

        public FileService(MirrorContext context, IMapper mapper, ILogger<FileService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Image> SaveFileToBlob(IFormFile file)
        {
            _logger.LogInformation("Saving file {FileName} to blob storage", file.FileName);

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var imageData = ms.ToArray();

            var mappedImage = _mapper.Map<Image>(file);
            mappedImage.Content = imageData;

            _context.Images.Add(mappedImage);
            await _context.SaveChangesAsync();

            _logger.LogInformation("File {FileName} saved successfully to blob storage with ID {ImageId}", file.FileName, mappedImage.Id);

            return mappedImage;
        }

        public async Task<string> SaveFileToDisk(IFormFile file, Guid memoryId)
        {
            _logger.LogInformation("Saving file {FileName} to disk for memory {MemoryId}", file.FileName, memoryId);

            var memoryPath = Path.Combine("C:/mirror/memories", memoryId.ToString());

            if (!Directory.Exists(memoryPath))
            {
                Directory.CreateDirectory(memoryPath);
                _logger.LogInformation("Created directory {DirectoryPath}", memoryPath);
            }

            var filePath = Path.Combine(memoryPath, file.FileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            _logger.LogInformation("File {FileName} successfully saved to disk at {FilePath}", file.FileName, filePath);

            return $"/mirror/memories/{memoryId}/{file.FileName}";
        }

        public async Task<string?> ValidateAndSaveFile(IFormFile file, Guid memoryId)
        {
            _logger.LogInformation("Validating file {FileName} for memory {MemoryId}", file.FileName, memoryId);

            if (file.Length > 5 * 1024 * 1024)
            {
                _logger.LogWarning("Validation failed for file {FileName}. File size exceeds limit.", file.FileName);
                throw new InvalidOperationException("File size exceeds limit.");
            }

            var validContentTypes = new[] { "image/jpeg", "image/png" };
            if (!validContentTypes.Contains(file.ContentType))
            {
                _logger.LogWarning("Validation failed for file {FileName}. Invalid file type {ContentType}.", file.FileName, file.ContentType);
                throw new InvalidOperationException("Invalid file type.");
            }

            _logger.LogInformation("File {FileName} passed validation. Saving to disk.", file.FileName);

            return await SaveFileToDisk(file, memoryId);
        }
    }
}