using Mirror.Domain.Entities;

namespace Mirror.Application.Services.Repository.ProgressValues
{
    public interface IProgressValueRepository
    {
        public Task<List<ProgressValue>> GetProgressValuesAsync();

        public Task<List<ProgressValue>> GetProgressValueByProgressAsync(Guid progressId);
    }
}
