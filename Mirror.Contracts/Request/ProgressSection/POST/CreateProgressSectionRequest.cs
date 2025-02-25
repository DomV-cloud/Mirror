using Mirror.Contracts.Request.ProgressValue.POST;

namespace Mirror.Contracts.Request.ProgressSection.POST
{
    /// <summary>
    /// Represents a request to create a new progress section.
    /// </summary>
    public class CreateProgressSectionRequest
    {
        /// <summary>
        /// Gets or sets the list of progress values associated with the progress section.
        /// </summary>
        /// <remarks>
        /// This list contains the progress values that will be tracked within the section.
        /// </remarks>
        public List<ProgressValueRequest>? ProgressValues { get; set; } = [];

        /// <summary>
        /// Gets or sets the name of the progress section.
        /// </summary>
        /// <example>Health Progress</example>
        public string SectionName { get; set; } = null!;
    }
}
