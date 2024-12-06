using Bogus;
using Mirror.Domain.Entities;

namespace Tests.MockData.Progress
{
    public static class FakeProgress
    {
        public static Faker<Mirror.Domain.Entities.Progress> GetProgressFaker(
            int progressValueCount = 5,
            string? progressName = "",
            string? description = "",
            List<ProgressValue>? progressValues = null)
        {
            return new Faker<Mirror.Domain.Entities.Progress>()
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.ProgressName, f => progressName ?? f.Lorem.Word())
                .RuleFor(p => p.Description, f => description ?? f.Lorem.Sentence(5))
                .RuleFor(p => p.UserId, f => Guid.NewGuid())
                .RuleFor(p => p.User, f => GenerateFakeUser())
                .RuleFor(p => p.ProgressValue, f => progressValues ?? GenerateProgressValues(progressValueCount));
        }

        public static List<ProgressValue> GenerateProgressValues(int count, Guid? progressId = null)
        {
            return new Faker<ProgressValue>()
                .RuleFor(v => v.Id, _ => Guid.NewGuid())
                .RuleFor(v => v.ProgressColumnHead, f => f.PickRandom(new[] { "Weight", "Height", "Steps" }))
                .RuleFor(v => v.ProgressColumnValue, f => f.Random.Double(10, 200).ToString("F2"))
                .RuleFor(v => v.ProgressId, _ => progressId ?? Guid.NewGuid())
                .RuleFor(v => v.ProgressDate_Day, f => f.Date.Recent().Day)
                .RuleFor(v => v.ProgressDate_Month, f => f.Date.Recent().Month)
                .RuleFor(v => v.ProgressDate_Year, f => f.Date.Recent().Year)
                .Generate(count);
        }

        public static User GenerateFakeUser()
        {
            return new Faker<User>()
                .RuleFor(u => u.Id, _ => Guid.NewGuid())
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => f.Internet.Password());
        }
    }
}
