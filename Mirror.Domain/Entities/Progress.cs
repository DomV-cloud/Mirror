using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Mirror.Domain.Entities
{
    public class Progress : BaseEntity
    {
        [Required]
        [JsonPropertyName("description")]
        public string Description { get; set; } = null!;

        [Required]
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public User User { get; set; } = null!;

        public List<ProgressValue> ProgressValue { get; set; } = [];
    }
}
