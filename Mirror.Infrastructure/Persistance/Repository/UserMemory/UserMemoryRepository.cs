using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mirror.Application.DatabaseContext;
using Mirror.Application.Services.Repository.Memory;

namespace Mirror.Infrastructure.Services.Repository.UserMemory
{
    public class UserMemoryRepository : IUserMemoryRepository
    {
        private readonly MirrorContext _context;
        private readonly ILogger<UserMemoryRepository> _logger;

        public UserMemoryRepository(MirrorContext context, ILogger<UserMemoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Domain.Entities.UserMemory> CreateMemory(Domain.Entities.UserMemory memoryToSave)
        {
            _logger.LogInformation("Attempting to create a new memory for user with ID: {UserId}", memoryToSave?.UserId);

            if (memoryToSave is null)
            {
                _logger.LogWarning("Memory object is null. Creation aborted.");
                return new();
            }

            await _context.Memories.AddAsync(memoryToSave);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Memory with ID: {MemoryId} successfully created for user with ID: {UserId}", memoryToSave.Id, memoryToSave.UserId);

            return memoryToSave;
        }

        public async Task<List<Domain.Entities.UserMemory>> GetAllMemoryByUserId(Guid userId)
        {
            _logger.LogInformation("Fetching all memories for user with ID: {UserId}", userId);

            var memories = await _context.Memories
                .AsNoTracking()
                .Include(memory => memory.Images)
                .Where(memory => memory.UserId == userId)
                .ToListAsync();

            if (memories is null || memories.Count == 0)
            {
                _logger.LogWarning("No memories found for user with ID: {UserId}", userId);
                return new List<Domain.Entities.UserMemory>();
            }

            _logger.LogInformation("{Count} memories found for user with ID: {UserId}", memories.Count, userId);
            return memories;
        }

        public async Task<Domain.Entities.UserMemory> GetMemoryById(Guid memoryId)
        {
            _logger.LogInformation("Fetching memory with ID: {MemoryId}", memoryId);

            var memory = await _context.Memories
                .AsNoTracking()
                .Include(memory => memory.Images)
                .FirstOrDefaultAsync(memory => memory.Id == memoryId);

            if (memory is null)
            {
                _logger.LogWarning("No memory found with ID: {MemoryId}", memoryId);
                return new();
            }

            _logger.LogInformation("Memory with ID: {MemoryId} successfully retrieved.", memoryId);
            return memory;
        }
    }
}
