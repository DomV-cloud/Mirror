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

        public async Task<Domain.Entities.UserMemory> CreateMemoryAsync(Domain.Entities.UserMemory memoryToSave)
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

        public async Task<bool> DeleteMemoryAsync(Domain.Entities.UserMemory memoryToDelete)
        {
            if (memoryToDelete is null)
            {
                return false;
            }

            _context.Memories.Remove(memoryToDelete);

            int updatedRows = await _context.SaveChangesAsync();

            if (updatedRows < 0)
            {
                return false;
            }

            return true;
        }

        public async Task<List<Domain.Entities.UserMemory>> GetAllMemoryByUserIdAsync(Guid userId)
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
                return [];
            }

            _logger.LogInformation("{Count} memories found for user with ID: {UserId}", memories.Count, userId);
            return memories;
        }

        public async Task<Domain.Entities.UserMemory> GetMemoryByIdAsync(Guid memoryId)
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

        public async Task<bool> UpdateMemoryAsync(Domain.Entities.UserMemory existingMemory, Domain.Entities.UserMemory newMemory)
        {
            if (existingMemory.Id == newMemory.Id)
            {
                return false;
            }

            //It works, but it seems to me unaffective. I am just thinking how it would look if entity has more and more properties....
            existingMemory.Description = newMemory.Description;
            existingMemory.Reminder = newMemory.Reminder;
            existingMemory.MemoryName = newMemory.MemoryName;

            if (newMemory.Images.Count != 0)
            {
                UpdateImages(existingMemory, newMemory.Images);
            }

            _context.Memories.Update(existingMemory);

            var entry = _context.Entry(existingMemory);
            if (entry.State == EntityState.Unchanged)
            {
                _logger.LogWarning($"No changes detected in {nameof(UpdateMemoryAsync)}.");
                return false;
            }

            int success = await _context.SaveChangesAsync();

            if (success == 0)
            {
                _logger.LogWarning($"No changes in {nameof(UpdateMemoryAsync)} were saved to the database");
                return false;
            }

            return true;
        }

        // For now I would leave it, but It am considering if it is right to do it like that. 
        // Updating images is same in Progress, I would consider to create maybe service
        private void UpdateImages(Domain.Entities.UserMemory existingMemory, List<Domain.Entities.Image> newImages)
        {
            foreach (var newImage in newImages)
            {
                existingMemory.Images.Add(newImage);
            }
        }
    }
}
