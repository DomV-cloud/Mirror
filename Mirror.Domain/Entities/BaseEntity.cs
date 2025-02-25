using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Mirror.Domain.Entities
{
    public class BaseEntity
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Date when was the entity is saved
        /// </summary>
        [JsonPropertyName("saved")]
        public DateTime SavedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Date when was the last time entity tracked/updated
        /// </summary>
        [JsonPropertyName("updated")]
        public DateTime? UpdatedDate { get; set; }
    }
}
