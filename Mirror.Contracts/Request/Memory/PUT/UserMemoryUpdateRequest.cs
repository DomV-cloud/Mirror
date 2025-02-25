using Microsoft.AspNetCore.Http;
using Mirror.Domain.Entities;
using System.Text.Json.Serialization;

namespace Mirror.Contracts.Request.Memory.PUT
{
    /// <summary>
    /// Represents a request to update an existing user memory.
    /// </summary>
    public class UserMemoryUpdateRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user who owns the memory.
        /// </summary>
        /// <example>123e4567-e89b-12d3-a456-426614174000</example>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the updated name of the memory.
        /// </summary>
        /// <remarks>
        /// This field is optional. If null, the name will not be updated.
        /// </remarks>
        /// <example>Updated Memory Name</example>
        public string? MemoryName { get; set; }

        /// <summary>
        /// Gets or sets the updated description of the memory.
        /// </summary>
        /// <remarks>
        /// This field is optional. If null, the description will not be updated.
        /// </remarks>
        /// <example>This is the updated description of my memory.</example>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the list of new images to add to the memory.
        /// </summary>
        /// <remarks>
        /// Each new image should be provided as a file uploaded by the user.
        /// </remarks>
        public List<IFormFile>? NewImages { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of image IDs to delete from the memory.
        /// </summary>
        /// <remarks>
        /// Provide the IDs of images that should be removed.
        /// </remarks>
        /// <example>[ "d2d4dabc-8d3e-4a8f-a6c3-dae3fd827ba7", "81bdb156-3f2d-4b1a-9536-f60f5467344a" ]</example>
        public List<Guid>? ImagesToDelete { get; set; } = [];

        /// <summary>
        /// Gets or sets the updated reminder associated with the memory.
        /// </summary>
        /// <remarks>
        /// This field is optional. If null, the reminder will not be updated.
        /// </remarks>
        /// <example>Don't forget to check this memory on 2025-03-01!</example>
        public string? Reminder { get; set; }
    }
}
