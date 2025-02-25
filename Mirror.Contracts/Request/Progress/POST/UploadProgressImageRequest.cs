namespace Mirror.Contracts.Request.Progress.POST
{
    /// <summary>
    /// Represents a request to upload an image for progress tracking.
    /// </summary>
    public class UploadProgressImageRequest
    {
        /// <summary>
        /// Gets or sets the name of the image.
        /// </summary>
        /// <example>ProgressImage.png</example>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the binary data of the image.
        /// </summary>
        /// <remarks>
        /// The image data should be provided as a byte array.
        /// </remarks>
        public byte[] Data { get; set; } = null!;

        /// <summary>
        /// Gets or sets the MIME type of the image.
        /// </summary>
        /// <remarks>
        /// This should be a valid MIME type such as "image/png" or "image/jpeg".
        /// </remarks>
        /// <example>image/png</example>
        public string ContentType { get; set; } = null!;
    }
}
