using Mirror.Contracts.Request.Images;

namespace Mirror.Contracts.Request.Memory.POST
{
    public record UserMemoryCreateRequest(
        Guid UserId,
        string MemoryName,
        string? Description,
        List<CreateImageRequest>? Images,
        string Reminder);
}
