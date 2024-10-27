using Mirror.Domain.Entities;
namespace Mirror.Application.Services.Repository.Progresses
{
    public interface IProgressRepository
    {
        public Task<List<Progress>> GetProgressesAsync();

        public Task<List<Progress>> GetProgressesByUserAsync(Guid userId);
    }
}
