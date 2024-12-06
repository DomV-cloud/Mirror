using Bogus;
using Mirror.Contracts.Request.ProgressValue;

namespace Tests.MockData.Progress
{
    public static class FakeProgressValue
    {
        public static List<ProgressValueDTO> GenerateProgressValueDTO(int count = 5)
        {
            return new Faker<ProgressValueDTO>()
                .CustomInstantiator(f => new ProgressValueDTO(
                    ProgressColumnHead: f.Lorem.Word(),
                    ProgressColumnValue: f.Random.Bool() ? f.Commerce.ProductName() : null,
                    ProgressDate_Day: f.Date.Recent().Day,
                    ProgressDate_Month: f.Date.Recent().Month,
                    ProgressDate_Year: f.Date.Recent().Year))
                .Generate(count);
        }
    }
}
