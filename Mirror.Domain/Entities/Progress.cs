using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Mirror.Domain.Entities
{
    /// <summary>
    /// Represents a progress entry linked to a user, allowing tracking of various progress metrics.
    /// </summary>
    public class Progress : BaseEntity
    {
        /// <summary>
        /// Name of the progress metric (e.g., weight, time).
        /// </summary>
        [Required]
        [JsonPropertyName("progressName")]
        public string ProgressName { get; set; } = null!;

        /// <summary>
        /// Additional description providing context for the progress metric.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Foreign key linking to the associated user.
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        /// <summary>
        /// Reference to the user entity.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// Collection of progress values representing tracked data points for the metric.
        /// </summary>
        [JsonPropertyName("progressValues")]
        public List<ProgressValue> ProgressValue { get; set; } = [];
    }
}
