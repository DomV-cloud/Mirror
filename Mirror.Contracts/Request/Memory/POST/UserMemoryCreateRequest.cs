using Microsoft.AspNetCore.Http;

namespace Mirror.Contracts.Request.Memory.POST
{
    /// <summary>
    /// Represents a request to create a new user memory.
    /// </summary>
    public class UserMemoryCreateRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user who owns the memory.
        /// </summary>
        /// <example>123e4567-e89b-12d3-a456-426614174000</example>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the memory.
        /// </summary>
        /// <example>My Vacation</example>
        public string MemoryName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the memory.
        /// </summary>
        /// <remarks>
        /// This field is optional and can be null if no description is provided.
        /// </remarks>
        /// <example>This is a description of my vacation to the beach.</example>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets a list of images associated with the memory.
        /// </summary>
        /// <remarks>
        /// Each image should be provided as a file uploaded by the user.
        /// </remarks>
        public List<IFormFile>? Images { get; set; } = [];

        /// <summary>
        /// Gets or sets a reminder associated with the memory.
        /// </summary>
        /// <example>Don't forget to revisit this memory on 2025-02-10!</example>
        public string Reminder { get; set; } = null!;
    }
}
