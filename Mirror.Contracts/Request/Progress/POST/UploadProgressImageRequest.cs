namespace Mirror.Contracts.Request.Progress.POST
{
    public class UploadProgressImageRequest
    {
        public string Name { get; set; } = null!;

        public byte[] Data { get; set; } = null!;

        public string ContentType { get; set; } = null!;
    }
}
