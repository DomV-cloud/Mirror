namespace Mirror.Contracts.Request.Images.POST
{
    public class CreateImageRequest
    {
        public string Name { get; set; } = null!;

        public byte[] Data { get; set; } = [];

        public string ContentType { get; set; } = null!;

        public Guid UserMemoryId { get; set; }
    }
}
