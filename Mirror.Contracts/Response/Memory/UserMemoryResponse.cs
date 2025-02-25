using Mirror.Contracts.Response.Image;

namespace Mirror.Contracts.Response.Memory
{
    /// <summary>
    /// Represents the response object for a user's memory.
    /// </summary>
    public class UserMemoryResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user associated with the memory.
        /// </summary>
        /// <example>123e4567-e89b-12d3-a456-426614174000</example>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the memory.
        /// </summary>
        /// <example>f8d6e1a8-4ab2-45d7-b13d-6d72ff327d15</example>
        public Guid MemoryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the memory.
        /// </summary>
        /// <example>Summer Vacation</example>
        public string MemoryName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the memory.
        /// </summary>
        /// <remarks>
        /// This field is optional and can be null if no description is provided.
        /// </remarks>
        /// <example>A beautiful trip to the beach last summer.</example>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the list of images associated with the memory.
        /// </summary>
        /// <remarks>
        /// The list contains images uploaded for the memory, represented as `ImageResponse`.
        /// </remarks>
        public List<ImageResponse>? Images { get; set; } = [];

        /// <summary>
        /// Gets or sets the reminder associated with the memory.
        /// </summary>
        /// <example>Remember to revisit this memory on 2025-03-01!</example>
        public string Reminder { get; set; } = null!;
    }
}
