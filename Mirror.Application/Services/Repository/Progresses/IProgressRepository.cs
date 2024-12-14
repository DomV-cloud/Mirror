using Mirror.Domain.Entities;
namespace Mirror.Application.Services.Repository.Progresses
{
    public interface IProgressRepository
    {
        public Task<List<Progress>> GetProgressesAsync();

        public Task<List<Progress>> GetProgressesByUserAsync(Guid userId);

        public Task<Progress> GetProgressesByIdAsync(Guid progressId);

        public Task<Progress> CreateProgressAsync(Progress progress);

        public Task<bool> UpdateProgress(Progress existinProgress, Progress progressToUpdate);
    }
}
