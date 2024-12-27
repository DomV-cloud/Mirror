namespace Mirror.Contracts.Request.Images.GET
{
    public record ImageResponse(string FileName, string Url, Guid Id, Guid UserMemoryId, string ContentType);

}
