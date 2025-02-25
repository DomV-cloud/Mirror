using Bogus;
using Mirror.Domain.Entities;

namespace Tests.MockData.Progress
{
    public static class FakeProgressValue
    {
        public static List<ProgressValue> GenerateProgressValue(int count = 5)
        {
            return new Faker<ProgressValue>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            //.RuleFor(p => p.ProgressColumnHead, f => f.Lorem.Word())
            .RuleFor(p => p.ProgressColumnValue, f => f.Random.Bool() ? f.Commerce.ProductName() : null)
            .RuleFor(p => p.ProgressDate_Day, f => f.Date.Recent().Day)
            .RuleFor(p => p.ProgressDate_Month, f => f.Date.Recent().Month)
            .RuleFor(p => p.ProgressDate_Year, f => f.Date.Recent().Year)
            //.RuleFor(p => p.ProgressId, f => Guid.NewGuid())
            //.RuleFor(p => p.Progress, f => null)
            .Generate(count);
        }
    }
}
