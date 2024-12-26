using Mirror.Domain.Entities;

namespace Mirror.Application.Services.Repository.Memory
{
    public interface IUserMemoryRepository
    {
        public Task<UserMemory> CreateMemory(UserMemory memoryToSave);
    }
}
