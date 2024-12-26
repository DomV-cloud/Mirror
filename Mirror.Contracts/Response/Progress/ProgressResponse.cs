using Mirror.Contracts.Request.ProgressValue;
using Mirror.Domain.Enums.Progress;

namespace Mirror.Contracts.Response.Progress
{
    public class ProgressResponse
    {
        public Guid CreatedProgressId { get; set; }

        public Dictionary<string, List<ProgressValueDTO>> ProgressValue = [];

        public string ProgressName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? TrackingProgressDays { get; set; }

        public double PercentageAchieved { get; set; }

        public bool? IsAchieved { get; set; }
    }
}
