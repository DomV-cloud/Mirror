using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Mirror.Domain.Entities
{
    /// <summary>
    /// Represents a single value entry for a progress metric, with date information.
    /// </summary>
    public class ProgressValue : BaseEntity
    {
        /// <summary>
        /// Foreign key linking to the associated section.
        /// </summary>
        [ForeignKey(nameof(Progress))]
        public Guid ProgressSectionId { get; set; }

        /// <summary>
        /// Reference to the user ProgressSection.
        /// </summary>
        public ProgressSection ProgressSection { get; set; } = null!;

        /// <summary>
        /// Actual recorded value for the metric (e.g., 70 kg).
        /// </summary>
        [JsonPropertyName("column-value")]
        public string? ProgressColumnValue { get; set; }

        ///// <summary>
        ///// Foreign key linking to the parent Progress entity.
        ///// </summary>
        //[ForeignKey("Progress")]
        //public Guid ProgressId { get; set; }

        ///// <summary>
        ///// Reference to the Progress entity this value belongs to.
        ///// </summary>
        //public Progress Progress { get; set; } = null!;

        /// <summary>
        /// Day part of the date for this progress entry.
        /// </summary>
        [JsonPropertyName("day")]
        public int ProgressDate_Day { get; set; }

        /// <summary>
        /// Month part of the date for this progress entry.
        /// </summary>
        [JsonPropertyName("month")]
        public int ProgressDate_Month { get; set; }

        /// <summary>
        /// Year part of the date for this progress entry.
        /// </summary>
        [JsonPropertyName("year")]
        public int ProgressDate_Year { get; set; }
    }
}
