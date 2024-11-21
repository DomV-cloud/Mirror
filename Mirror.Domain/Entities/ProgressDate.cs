using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Mirror.Domain.Entities
{
    [Owned]
    public class ProgressDate : BaseEntity
    {
        [JsonPropertyName("day")]
        public int Day { get; set; }

        [JsonPropertyName("month")]
        public int Month { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }
    }
}
