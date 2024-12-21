using Mirror.Contracts.Request.ProgressValue;
using Mirror.Domain.Entities;

namespace Mirror.Contracts.Response.Progress
{
    public class ProgressResponse
    {
        public Guid CreatedProgressId { get; set; }

        public Dictionary<string, List<ProgressValueDTO>> ProgressValue = [];

        public string ProgressName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ProgressColumnHead { get; set; } = null!;
    }
}
