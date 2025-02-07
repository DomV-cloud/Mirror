using Mirror.Contracts.Request.ProgressValue;

namespace Mirror.Contracts.Response.Section
{
    public class ProgressSectionResponse
    {
        public Guid Id{ get; set; }

        public string SectionName { get; set; } = null!;

        public List<ProgressValueResponse> ProgressValues { get; set; } = [];
    }
}
