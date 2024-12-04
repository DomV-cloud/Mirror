using Mirror.Contracts.Request.ProgressValue;

namespace Mirror.Contracts.Response.Progress
{
    public class CreatedProgressResponse
    {
        public Guid CreatedProgressId { get; set; }

        public List<ProgressValueDTO> ProgressValue { get; set; } = [];

        public string ProgressName { get; set; } = null!;
    }
}
