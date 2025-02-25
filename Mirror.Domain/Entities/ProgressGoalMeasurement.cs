using Mirror.Domain.Enums.Progress;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Mirror.Domain.Entities
{
    public class ProgressGoalMeasurement : BaseEntity
    {
        /// <summary>
        /// Foreign key linking to the associated ProgressGoal.
        /// </summary>
        [ForeignKey(nameof(ProgressGoal))]
        public Guid ProgressGoalId { get; set; }

        /// <summary>
        /// Reference to the ProgressGoal entity.
        /// </summary>
        public ProgressGoal ProgressGoal { get; set; } = null!;

        /// <summary>
        /// Day of the week when the measurement occurs.
        /// </summary>
        [JsonPropertyName("measurementDay")]
        public MeasurementDay MeasurementDay { get; set; } = MeasurementDay.Monday;

        /// <summary>
        /// The next scheduled measurement date.
        /// </summary>
        [JsonPropertyName("nextMeasurementDate")]
        public DateTime NextMeasurementDate { get; set; }

        /// <summary>
        /// Number of days remaining until the next measurement.
        /// </summary>
        [JsonPropertyName("daysRemaining")]
        public int DaysRemaining => (NextMeasurementDate - DateTime.UtcNow).Days;
    }
}
