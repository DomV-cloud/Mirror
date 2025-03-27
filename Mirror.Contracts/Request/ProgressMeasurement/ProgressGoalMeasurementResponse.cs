using Mirror.Domain.Enums.Progress;

namespace Mirror.Contracts.Request.ProgressMeasurement
{
    public record ProgressGoalMeasurementResponse
    (
        string MeasurementDay,
        DateTime NextMeasurementDate,
        int DaysRemaining
        );
}
