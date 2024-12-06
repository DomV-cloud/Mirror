using Mirror.Contracts.Request.ProgressValue;

namespace Mirror.Contracts.Request.Progress
{
    public record CreateProgressResponse(
        string ProgressName,
        List<ProgressValueDTO>? ProgressValue,
        Guid UserId
        );
}
