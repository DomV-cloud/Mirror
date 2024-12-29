using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Mirror.Domain.Entities
{
    public class Image : BaseEntity
    {
        /// <summary>
        /// Name of the image
        /// </summary>
        [JsonPropertyName("imageName")]
        public string FileName { get; set; } = null!;

        /// <summary>
        /// Binární data obrázku.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("content")]
        public byte[] Content { get; set; } = [];

        /// <summary>
        /// Content type of the image
        /// </summary>
        [JsonPropertyName("imageContentType")]
        public string ContentType { get; set; } = null!;

        /// <summary>
        /// Id of the user's memory
        /// </summary>
        [JsonPropertyName("memoryId")]
        [ForeignKey(nameof(UserMemory))]
        public Guid? UserMemoryId { get; set; }
        
        public UserMemory? UserMemory { get; set; }
    }
}
