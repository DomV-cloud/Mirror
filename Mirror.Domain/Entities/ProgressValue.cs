using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mirror.Domain.Entities
{
    public class ProgressValue : BaseEntity
    {
        [JsonPropertyName("column-head")]
        public string? ProgressColumnHead { get; set; }

        [JsonPropertyName("column-value")]
        public string? ProgressColumnValue { get; set; }

        [ForeignKey("Progress")]
        public Guid ProgressId { get; set; }

        public Progress Progress { get; set; } = null!;
    }
}
