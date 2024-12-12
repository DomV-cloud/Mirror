using Mirror.Contracts.Request.ProgressValue;

namespace Mirror.Contracts.Response.Progress
{
    public class ProgressResponse
    {
        public Guid CreatedProgressId { get; set; }

        public List<ProgressValueDTO> ProgressValue { get; set; } = [];

        public string ProgressName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ProgressColumnHead { get; set; } = null!;
    }
}
