using Microsoft.AspNetCore.Http;
using Mirror.Contracts.Request.ProgressValue;

namespace Mirror.Contracts.Request.Progress.POST
{
    public record CreateProgressRequest(
        string ProgressName,
        List<ProgressValueDTO>? ProgressValue,
        Guid UserId,
        bool? IsAchieved,
        double? TrackedDays,
        string? TrackingProgressDays,
        double? PercentageAchieved,
        DateTime? Updated,
        IFormFile? image
        );
}
