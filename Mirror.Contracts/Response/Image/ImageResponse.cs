namespace Mirror.Contracts.Response.Image
{
    /// <summary>
    /// Represents the response object for an image.
    /// </summary>
    public class ImageResponse
    {
        /// <summary>
        /// Gets or sets the name of the image file.
        /// </summary>
        /// <example>example_image.png</example>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the URL where the image can be accessed.
        /// </summary>
        /// <example>https://example.com/images/example_image.png</example>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier of the image.
        /// </summary>
        /// <example>c29ee4d0-7c53-4e4a-911e-f388df687d9d</example>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user memory associated with the image.
        /// </summary>
        /// <example>c2f35d58-24b6-4c77-9f02-cf1fdc5bafcb</example>
        public Guid UserMemoryId { get; set; }

        /// <summary>
        /// Gets or sets the MIME type of the image.
        /// </summary>
        /// <example>image/png</example>
        public string ContentType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the binary content of the image.
        /// </summary>
        /// <remarks>
        /// The image content is provided as a byte array.
        /// </remarks>
        public byte[] Content { get; set; } = [];
    }
}
