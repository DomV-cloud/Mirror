using Bogus;
using Mirror.Contracts.Request.Memory.POST;
using Mirror.Contracts.Request.Memory.PUT;
using Mirror.Contracts.Response.Image;
using Mirror.Contracts.Response.Memory;
using Mirror.Domain.Entities;
using Mirror.Domain.Enums.UserMemory;
using System.Drawing;
using Tests.MockData.Image;

namespace Tests.MockData.Memory
{
    public static class FakeUserMemory
    {
        public static Faker<UserMemoryCreateRequest> CreateMockUserMemoryRequest()
        {
            return new Faker<UserMemoryCreateRequest>()
        .RuleFor(x => x.UserId, f => f.Random.Guid())
        .RuleFor(x => x.MemoryName, f => f.Lorem.Sentence(3))
        .RuleFor(x => x.Description, f => f.Lorem.Paragraph())
        .RuleFor(x => x.Images, f => FakeImage.GenerateMockImages(f.Random.Number(1, 5)))
        .RuleFor(x => x.Reminder, f => f.PickRandom(new[] { "Daily", "Weekly", "Monthly", "Yearly" }));
        }

        public static Faker<UserMemoryUpdateRequest> UpdateMockUserMemoryRequest(
            List<Guid>? imagesToDelete = null,
            List<Guid>? existingImages = null)
        {
            return new Faker<UserMemoryUpdateRequest>()
        .RuleFor(x => x.UserId, f => f.Random.Guid())
        .RuleFor(x => x.MemoryName, f => f.Lorem.Sentence(3))
        .RuleFor(x => x.Description, f => f.Lorem.Paragraph())
        .RuleFor(x => x.NewImages, f => FakeImage.GenerateMockImages(f.Random.Number(1, 5)))
        .RuleFor(x => x.ImagesToDelete, f => imagesToDelete ?? null)
        .RuleFor(x => x.Reminder, f => f.PickRandom(new[] { "Daily", "Weekly", "Monthly", "Yearly" }));
        }

        public static Faker<UserMemory> CreateMockUserMemory(
            Guid? id = null,
            Guid? userId = null,
            string? memoryName = "",
            string? description = "",
            Reminder? reminder = Reminder.Daily,
            List<Mirror.Domain.Entities.Image>? images = null
            )
        {
            return new Faker<UserMemory>()
           .RuleFor(um => um.Id, f => id ?? Guid.NewGuid())
           .RuleFor(um => um.UserId, f => userId ?? Guid.NewGuid())
           .RuleFor(um => um.MemoryName, f => memoryName ?? f.Lorem.Sentence(3))
           .RuleFor(um => um.Description, f => description ?? f.Lorem.Paragraph())
           .RuleFor(um => um.Reminder, f => reminder ?? f.PickRandom<Reminder>())
           .RuleFor(um => um.Images, f => images ?? FakeImage.CreateMockImage().Generate(f.Random.Int(1, 5)));
        }

        public static Faker<UserMemoryResponse> CreateMockUserMemoryResponse(
            Guid? userId = null,
            Guid? memoryId = null,
            string? memoryName = "",
            string? description = "",
            string? reminder = "",
            List<ImageResponse>? images = null
            )
        {
            return new Faker<UserMemoryResponse>()
           .RuleFor(um => um.UserId, f => userId ?? Guid.NewGuid())
           .RuleFor(um => um.MemoryId, f => memoryId ?? Guid.NewGuid())
           .RuleFor(um => um.MemoryName, f => memoryName ?? f.Lorem.Sentence(3))
           .RuleFor(um => um.Description, f => description ?? f.Lorem.Paragraph())
           .RuleFor(um => um.Reminder, f => reminder ?? f.PickRandom(new[] { "Daily", "Weekly", "Monthly", "Yearly" }))
           .RuleFor(um => um.Images, f => images ?? FakeImage.CreateMockImageResponse().Generate(f.Random.Int(1, 5)));
        }
    }
}
