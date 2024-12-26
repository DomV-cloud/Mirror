using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mirror.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = null!;

        [Required]
        [JsonPropertyName("LastName")]
        public string LastName { get; set; } = null!;

        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;

        [JsonPropertyName("progress")]
        public List<Progress>? Progresses { get; set; } = [];
    }
}
