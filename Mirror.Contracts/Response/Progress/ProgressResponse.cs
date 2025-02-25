using Mirror.Contracts.Response.Section;
using Mirror.Domain.Entities;

namespace Mirror.Contracts.Response.Progress
{
    /// <summary>
    /// Represents the response object for a progress record.
    /// </summary>
    public class ProgressResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the progress record.
        /// </summary>
        /// <example>e3c6bff4-5db4-4a22-b69d-028c4d9d6174</example>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the created progress.
        /// </summary>
        /// <example>f402e2e9-17b5-4b80-92d4-975e1f1b1f4f</example>
        public Guid CreatedProgressId { get; set; }

        /// <summary>
        /// Gets or sets the list of progress sections associated with the progress.
        /// </summary>
        /// <remarks>
        /// The list contains sections that break down the overall progress, represented as `ProgressSectionResponse`.
        /// </remarks>
        public List<ProgressSectionResponse> Sections = [];

        /// <summary>
        /// Gets or sets the name of the progress.
        /// </summary>
        /// <example>Fitness Journey</example>
        public string ProgressName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the progress.
        /// </summary>
        /// <remarks>
        /// This field is optional and can be null if no description is provided.
        /// </remarks>
        /// <example>A personal progress journey for improving fitness over the course of a year.</example>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the string that tracks the days of progress.
        /// </summary>
        /// <remarks>
        /// This field is optional and can be used to track days associated with progress.
        /// </remarks>
        /// <example>"Day 10 of 30"</example>
        public string? TrackingProgressDays { get; set; }

        /// <summary>
        /// Gets or sets the percentage of progress achieved.
        /// </summary>
        /// <example>75.5</example>
        public double PercentageAchieved { get; set; }

        /// <summary>
        /// Gets or sets whether the progress has been achieved.
        /// </summary>
        /// <remarks>
        /// This field is optional and indicates if the progress goal has been met.
        /// </remarks>
        public bool? IsAchieved { get; set; }

        /// <summary>
        /// Gets or sets whether the progress is active.
        /// </summary>
        /// <remarks>
        /// This field is optional and indicates whether the progress is still ongoing or completed.
        /// </remarks>
        public bool? IsActive { get; set; }
    }
}
