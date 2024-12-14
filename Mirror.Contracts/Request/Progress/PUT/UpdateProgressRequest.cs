using Mirror.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mirror.Contracts.Request.Progress.PUT
{
    public class UpdateProgressRequest
    {
        /// <summary>
        /// Id of the request DTO
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Name of the progress metric (e.g., weight, time).
        /// </summary>
        [JsonPropertyName("progressName")]
        public string? ProgressName { get; set; } = null!;

        /// <summary>
        /// Additional description providing context for the progress metric.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Foreign key linking to the associated user.
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }

        /// <summary>
        /// Reference to the user entity.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// Collection of progress values representing tracked data points for the metric.
        /// </summary>
        [JsonPropertyName("progressValues")]
        public List<Mirror.Domain.Entities.ProgressValue>? ProgressValue { get; set; } = [];
    }
}
