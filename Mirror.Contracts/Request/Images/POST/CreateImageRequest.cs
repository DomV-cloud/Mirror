namespace Mirror.Contracts.Request.Images.POST
{
    /// <summary>
    /// Represents a request to create a new image.
    /// </summary>
    public class CreateImageRequest
    {
        /// <summary>
        /// Gets or sets the name of the image.
        /// </summary>
        /// <example>ExampleImageName</example>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the binary data of the image.
        /// </summary>
        /// <remarks>
        /// The image data should be provided as a byte array.
        /// </remarks>
        public byte[] Data { get; set; } = [];

        /// <summary>
        /// Gets or sets the MIME type of the image.
        /// </summary>
        /// <example>image/png</example>
        public string ContentType { get; set; } = null!;

        /// <summary>
        /// Gets or sets the unique identifier of the user memory associated with the image.
        /// </summary>
        /// <example>123e4567-e89b-12d3-a456-426614174000</example>
        public Guid UserMemoryId { get; set; }
    }
}
