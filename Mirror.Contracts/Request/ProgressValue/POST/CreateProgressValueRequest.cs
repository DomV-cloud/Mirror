namespace Mirror.Contracts.Request.ProgressValue.POST
{
    public record ProgressValueRequest(
        string? ProgressColumnValue,
        int ProgressDate_Day,
        int ProgressDate_Month,
        int ProgressDate_Year
        );
}
