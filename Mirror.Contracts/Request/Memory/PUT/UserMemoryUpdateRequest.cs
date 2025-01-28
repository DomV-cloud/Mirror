using Microsoft.AspNetCore.Http;
using Mirror.Domain.Entities;

namespace Mirror.Contracts.Request.Memory.PUT
{
    public class UserMemoryUpdateRequest
    {
        public Guid UserId { get; set; }

        public string? MemoryName { get; set; }

        public string? Description { get; set; }

        public IFormFileCollection? NewImages { get; set; }

        public List<Guid>? ImagesToDelete { get; set; } = [];

        public List<Guid>? ExistingImageIds { get; set; } = [];

        public string? Reminder { get; set; }
    }
}
