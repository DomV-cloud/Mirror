using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mirror.Application.DatabaseContext;
using Mirror.Domain.Entities;

namespace Mirror.Application.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly MirrorContext _context;
        private readonly IMapper _mapper;

        public FileService(MirrorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<Image> GetFileFromBlobByUserId(Image image, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Image>> GetFilesFromBlob(Image image)
        {
            throw new NotImplementedException();
        }

        public async Task<Image> SaveFileToBlob(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var imageData = ms.ToArray();

            var mappedImage = _mapper.Map<Image>(file);
            //mappedImage.Content = imageData;

            _context.Images.Add(mappedImage);
            await _context.SaveChangesAsync();

            return mappedImage;
        }

        public async Task<string> SaveFileToDisk(IFormFile file, Guid memoryId)
        {
            var uploadPath = Path.Combine("wwwroot/images/memories", memoryId.ToString());
            Directory.CreateDirectory(uploadPath);

            var filePath = Path.Combine(uploadPath, file.FileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/images/memories/{memoryId}/{file.FileName}";
        }

        public async Task<string?> ValidateAndSaveFile(IFormFile file, Guid memoryId)
        {
            if (file.Length > 5 * 1024 * 1024) // Max 5 MB
                throw new InvalidOperationException("File size exceeds limit.");

            var validContentTypes = new[] { "image/jpeg", "image/png" };
            if (!validContentTypes.Contains(file.ContentType))
                throw new InvalidOperationException("Invalid file type.");

            return await SaveFileToDisk(file, memoryId);
        }
    }
}
