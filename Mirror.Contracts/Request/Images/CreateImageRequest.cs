namespace Mirror.Contracts.Request.Images
{
    public record CreateImageRequest(
        string Name,
        byte[] Data,
        string ContentType,
        Guid UserMemoryId
        );
}
