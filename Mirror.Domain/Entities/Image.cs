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
        public string Name { get; set; } = null!;

        /// <summary>
        /// Image's data
        /// </summary>
        [JsonPropertyName("imageData")]
        public byte[] Data { get; set; } = null!;

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
