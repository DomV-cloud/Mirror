using Mirror.Domain.Entities;

namespace Mirror.Application.Services.Repository.Memory
{
    public interface IUserMemoryRepository
    {
        public Task<UserMemory> CreateMemory(UserMemory memoryToSave);

        public Task<List<UserMemory>> GetAllMemoryByUserId(Guid userId);

        public Task<UserMemory> GetMemoryById(Guid memoryId);
    }
}
