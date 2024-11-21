using Mirror.Contracts.ProgressValue;

namespace Mirror.Contracts.Progress
{
    public record CreateProgressDTO(
        string ProgressName,
        List<ProgressValueDTO> ProgressValue,
        Guid UserId
        );
}
