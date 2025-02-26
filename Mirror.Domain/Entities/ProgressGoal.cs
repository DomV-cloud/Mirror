using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Mirror.Domain.Entities
{
    public class ProgressGoal : BaseEntity
    {
        /// <summary>
        /// Shows if the user already achieved the progress
        /// </summary>
        [JsonPropertyName("isAchieved")]
        public bool? IsAchieved { get; set; }

        /// <summary>
        /// How many days is progress tracked
        /// </summary>
        [JsonPropertyName("trackedDays")]
        public double TrackedDays { get; set; } = 0;

        /// <summary>
        /// How many percentage user already achieved progress
        /// </summary>
        [JsonPropertyName("percentageAchieved")]
        public double PercentageAchieved { get; set; } = 0;

        /// <summary>
        /// Foreign key linking to the parent Progress entity.
        /// </summary>
        [ForeignKey(nameof(Progress))]
        public Guid ProgressId { get; set; }

        /// <summary>
        /// Reference to the Progress entity.
        /// </summary>
        public Progress Progress { get; set; } = null!;

        /// <summary>
        /// Reference to the ProgressGoalMeasurement entity.
        /// </summary>
        public ProgressGoalMeasurement Measurement { get; set; } = null!;
    }
}
