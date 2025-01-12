using Mirror.Domain.Entities;

namespace Mirror.Application.Services.Repository.Memory
{
    public interface IUserMemoryRepository
    {
        public Task<UserMemory> CreateMemoryAsync(UserMemory memoryToSave);

        public Task<List<UserMemory>> GetAllMemoryByUserIdAsync(Guid userId);

        public Task<UserMemory> GetMemoryByIdAsync(Guid memoryId);

        public Task<bool> UpdateMemoryAsync(UserMemory existingMemory, UserMemory memoryToUpdate);

        public Task<bool> DeleteMemoryAsync(UserMemory memoryToDelete);
    }
}
