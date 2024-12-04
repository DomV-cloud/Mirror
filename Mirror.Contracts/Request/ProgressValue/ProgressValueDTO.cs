namespace Mirror.Contracts.Request.ProgressValue
{
    public record ProgressValueDTO(
        string? ProgressColumnHead,
        string? ProgressColumnValue,
        int ProgressDate_Day,
        int ProgressDate_Month,
        int ProgressDate_Year
        );
}
