using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mirror.Application.DatabaseContext;
using Mirror.Application.Services.FileService.FilePathGenerator;
using Mirror.Domain.Entities;

namespace Mirror.Application.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
        private readonly IFilePathGenerator _filePathGenerator;

        public FileService(ILogger<FileService> logger, IFilePathGenerator filePathGenerator)
        {
            _logger = logger;
            _filePathGenerator = filePathGenerator;
        }

        public Task DeleteFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public string SaveFileToBlob(IFormFile file)
        {
            _logger.LogInformation("Saving file {FileName} to blob storage", file.FileName);

            var filePath = _filePathGenerator.GenerateFilePath(file.FileName);

            if (String.IsNullOrEmpty(filePath))
            {
                _logger.LogError("File {FileName} cannot save successfully with path is null or empty", file.FileName);
                return "";
            }
            _logger.LogInformation("File {FileName} saved successfully with path {FilePath}", file.FileName, filePath);

            return filePath;
        }
    }
}