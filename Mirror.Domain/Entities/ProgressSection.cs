using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Mirror.Domain.Entities
{
    public class ProgressSection : BaseEntity
    {
        [ForeignKey(nameof(Progress))]
        public Guid ProgressId { get; set; }

        public Progress Progress { get; set; } = null!;

        [JsonPropertyName("sectionName")]
        public string SectionName { get; set; } = null!;

        [JsonPropertyName("progressValues")]
        public List<ProgressValue> ProgressValues { get; set; } = [];
    }
}
