using Microsoft.AspNetCore.Http;
using Mirror.Contracts.Request.Images.GET;
using Mirror.Domain.Entities;

namespace Mirror.Contracts.Response.Memory
{
    public class UserMemoryResponse
    {
        public Guid UserId { get; set; }

        public string MemoryName { get; set; } = null!;

        public string? Description { get; set; }

        public List<ImageResponse>? Images { get; set; } = [];

        public string Reminder { get; set; } = null!;
    }
}
