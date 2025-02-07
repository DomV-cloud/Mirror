using Microsoft.AspNetCore.Http;
using Mirror.Contracts.Request.ProgressValue;

namespace Mirror.Contracts.Request.Progress.POST
{
    public record CreateProgressRequest(
        string ProgressName,
        List<ProgressValueResponse>? ProgressValue,
        Guid UserId,
        bool? IsAchieved,
        bool? IsActive,
        double? TrackedDays,
        string? TrackingProgressDays,
        double? PercentageAchieved,
        DateTime? Updated,
        IFormFile? image
        );
}
