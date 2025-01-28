using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mirror.Domain.Entities
{
    public class User : BaseEntity
    {
        /// <summary>
        /// First name of the user
        /// </summary>
        [Required]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Last name of the user
        /// </summary>
        [Required]
        [JsonPropertyName("LastName")]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Email of the user
        /// </summary>
        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Password of the user
        /// </summary>
        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;

        /// <summary>
        /// Progresses of the users
        /// </summary>
        [JsonPropertyName("progress")]
        public List<Progress>? Progresses { get; set; } = [];

        /// <summary>
        /// Memories of the user
        /// </summary>
        public List<UserMemory>? Memories { get; set; } = [];
    }
}
