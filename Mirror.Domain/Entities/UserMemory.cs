using Mirror.Domain.Enums.UserMemory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Mirror.Domain.Entities
{
    public class UserMemory : BaseEntity
    {
        /// <summary>
        /// Name of the memory
        /// </summary>
        [JsonPropertyName("memoryName")]
        public string MemoryName { get; set; } = null!;

        /// <summary>
        /// Description of the memory
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// List of images belongs to memory
        /// </summary>
        [JsonPropertyName("memoryImages")]
        public List<Image> Images { get; set; } = [];

        /// <summary>
        /// Value how often should user memory remind. Default value is 'monthly'
        /// </summary>
        [JsonPropertyName("reminder")]
        public Reminder SetReminder { get; set; } = Reminder.monthly;

        /// <summary>
        /// Id of the user
        /// </summary>
        [JsonPropertyName("userId")]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User User { get; set; } = null!;
    }
}
