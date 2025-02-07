using Mirror.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Mirror.Contracts.Request.ProgressValue;

namespace Mirror.Contracts.Request.Progress.PUT
{
    public class UpdateProgressRequest
    {
        /// <summary>
        /// Name of the progress metric (e.g., weight, time).
        /// </summary>
        [JsonPropertyName("progressName")]
        public string? ProgressName { get; set; }

        /// <summary>
        /// Additional description providing context for the progress metric.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Foreign key linking to the associated user.
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Collection of progress values representing tracked data points for the metric.
        /// </summary>
        [JsonPropertyName("progressValues")]
        public Dictionary<string, List<ProgressValueResponse>>? ProgressValues { get; set; } = [];
    }
}
