namespace Mirror.Contracts.Request.Images.GET
{
    public class ImageResponse
    {
        public string FileName { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public Guid UserMemoryId { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public byte[] Content { get; set; } = [];
    }
}
