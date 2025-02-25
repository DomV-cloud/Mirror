using Mirror.Contracts.Response.ProgressValue;

namespace Mirror.Contracts.Response.Section
{
    /// <summary>
    /// Represents the response object for a progress section.
    /// </summary>
    public class ProgressSectionResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the progress section.
        /// </summary>
        /// <example>f8a2d3b1-8b8b-4f73-a432-9b17b44c25b5</example>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the progress section.
        /// </summary>
        /// <example>Physical Health</example>
        public string SectionName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the list of progress values associated with this section.
        /// </summary>
        /// <remarks>
        /// The list contains the individual progress values tracked within the section, represented as `ProgressValueResponse`.
        /// </remarks>
        public List<ProgressValueResponse> ProgressValues { get; set; } = [];
    }
}
