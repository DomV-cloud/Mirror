namespace Mirror.Contracts.Response.Progress
{
    public record StatsProgressResponse(
        Guid CreatedProgressId,
        string ProgressName,
        bool? IsAchieved,
        double TrackedDays,
        DateTime RegularTrackingDateTime,
        double PercentageAchieved
        );

}
