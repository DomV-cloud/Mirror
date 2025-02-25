using Microsoft.AspNetCore.Http;
using Mirror.Contracts.Request.ProgressSection.POST;

namespace Mirror.Contracts.Request.Progress.POST
{
    public record CreateProgressRequest(
        string ProgressName,
        List<CreateProgressSectionRequest>? Sections,
        Guid UserId,
        bool? IsAchieved,
        bool? IsActive,
        double? TrackedDays,
        string? TrackingProgressDays,
        double? PercentageAchieved,
        DateTime? Updated,
        IFormFile? Image
        );
}
