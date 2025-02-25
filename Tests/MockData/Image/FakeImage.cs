using Bogus;
using Microsoft.AspNetCore.Http;
using Mirror.Contracts.Response.Image;

namespace Tests.MockData.Image
{
    public static class FakeImage
    {
        public static List<IFormFile> GenerateMockImages(int count)
        {
            var images = new List<IFormFile>();

            for (int i = 0; i < count; i++)
            {
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                writer.Write("Mock image content");
                writer.Flush();
                stream.Position = 0;

                var file = new FormFile(stream, 0, stream.Length, "MockImage", $"mock_image_{i + 1}.png")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/png"
                };

                images.Add(file);
            }

            return images;
        }

        public static Faker<Mirror.Domain.Entities.Image> CreateMockImage(
            string? filename = "",
            string? filePath = "",
            byte[]? content = null,
            string? contentType = "",
            string? url = ""
            )
        {
            return new Faker<Mirror.Domain.Entities.Image>()
            .RuleFor(i => i.Id, f => Guid.NewGuid())
            .RuleFor(i => i.FileName, f => filename ?? f.System.FileName())
            .RuleFor(i => i.Content, f => content ?? f.Random.Bytes(1024))
            .RuleFor(i => i.FilePath, f => filePath ?? f.System.FilePath())
            .RuleFor(i => i.ContentType, f => contentType ?? f.PickRandom(new[] { "image/png", "image/jpeg" }))
            .RuleFor(i => i.Url, f => url ?? f.Internet.Url())
            .RuleFor(i => i.UserMemoryId, f => f.Random.Guid());
        }

        public static Faker<ImageResponse> CreateMockImageResponse(
            Guid? id = null,
            string? fileName = "",
            byte[]? content = null,
            string? contentType = "",
            string? url = "",
            Guid? userMemoryId = null)
        {
            return new Faker<ImageResponse>()
            .RuleFor(i => i.Id, f => id ?? Guid.NewGuid())
            .RuleFor(i => i.FileName, f => fileName ?? f.System.FileName())
            .RuleFor(i => i.Content, f => content ?? f.Random.Bytes(1024))
            .RuleFor(i => i.ContentType, f => contentType ?? f.PickRandom(new[] { "image/png", "image/jpeg" }))
            .RuleFor(i => i.Url, f => url ?? f.Internet.Url())
            .RuleFor(i => i.UserMemoryId, f => userMemoryId ?? f.Random.Guid());
        }
    }
}
