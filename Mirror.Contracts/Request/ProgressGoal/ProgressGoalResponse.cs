using Mirror.Contracts.Request.ProgressMeasurement;

namespace Mirror.Contracts.Request.ProgressGoal
{
    public record ProgressGoalResponse
    (
         ProgressGoalMeasurementResponse Measurement,
         bool? IsAchieved,
         int TrackedDays = 0,
         double PercentageAchieved = 0
        );
}
