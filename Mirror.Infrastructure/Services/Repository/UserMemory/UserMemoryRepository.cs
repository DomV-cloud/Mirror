using Mirror.Application.DatabaseContext;
using Mirror.Application.Services.Repository.Memory;

namespace Mirror.Infrastructure.Services.Repository.UserMemory
{
    public class UserMemoryRepository : IUserMemoryRepository
    {
        private MirrorContext _context;

        public UserMemoryRepository(MirrorContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.UserMemory> CreateMemory(Domain.Entities.UserMemory memoryToSave)
        {
            if (memoryToSave is null)
            {
                return new();
            }

            await _context.Memories.AddAsync(memoryToSave);
            await _context.SaveChangesAsync();

            return memoryToSave;
        }
    }
}
