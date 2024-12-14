using Mirror.Contracts.Request.ProgressValue;

namespace Mirror.Contracts.Request.Progress.POST
{
    public record CreateProgressRequest(
        string ProgressName,
        List<ProgressValueDTO>? ProgressValue,
        Guid UserId
        );
}
