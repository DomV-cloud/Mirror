using Mirror.Contracts.Request.ProgressValue;

namespace Mirror.Contracts.Request.Progress
{
    public record CreateProgressDTO(
        string ProgressName,
        List<ProgressValueDTO>? ProgressValue,
        Guid UserId
        );
}
