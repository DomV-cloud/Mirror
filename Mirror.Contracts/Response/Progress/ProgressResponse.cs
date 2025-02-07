using Mirror.Contracts.Response.Section;
using Mirror.Domain.Entities;

namespace Mirror.Contracts.Response.Progress
{
    public class ProgressResponse
    {
        public Guid Id { get; set; }

        public Guid CreatedProgressId { get; set; }

        public List<ProgressSectionResponse> Sections = [];

        public string ProgressName { get; set; } = null!;

        public string? Description { get; set; }

        public string? TrackingProgressDays { get; set; }

        public double PercentageAchieved { get; set; }

        public bool? IsAchieved { get; set; }

        public bool? IsActive { get; set; }
    }
}
