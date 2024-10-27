using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mirror.Domain.Entities
{
    public class BaseEntity
    {
        [Required]
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
