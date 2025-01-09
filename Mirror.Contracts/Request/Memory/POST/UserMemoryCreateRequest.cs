using Microsoft.AspNetCore.Http;

namespace Mirror.Contracts.Request.Memory.POST
{
    public class UserMemoryCreateRequest
    {
        public Guid UserId { get; set; }

        public string MemoryName { get; set; } = null!;
        
        public string? Description { get; set; }
       
        public List<IFormFile>? Images { get; set; } = [];
        
        public string Reminder { get; set; } = null!;
    }
}
