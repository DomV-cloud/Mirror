namespace Mirror.Application.Services.FileService.FilePathGenerator
{
    public class FilePathGenerator : IFilePathGenerator
    {
        public string GenerateFilePath(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                return "";
            }

            return $"{fileName}/{DateTime.UtcNow}/{Guid.NewGuid()}";
        }
    }
}
