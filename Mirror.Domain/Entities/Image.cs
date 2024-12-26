namespace Mirror.Domain.Entities
{
    public class Image : BaseEntity
    {
        public string Name { get; set; } = null!;

        public byte[] Data { get; set; } = null!;

        public string ContentType { get; set; } = null!;
    }
}
