﻿using Microsoft.AspNetCore.Http;
using Mirror.Contracts.Request.Images;

namespace Mirror.Contracts.Request.Memory.POST
{
    public record UserMemoryCreateRequest
    {
        public Guid UserId { get; set; }

        public string MemoryName { get; set; } = null!;
        
        public string? Description { get; set; }
       
        public List<IFormFile>? Images { get; set; } = [];
        
        public string Reminder { get; set; } = null!;
    }
}