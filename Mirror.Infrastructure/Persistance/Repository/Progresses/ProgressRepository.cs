using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mirror.Application.DatabaseContext;
using Mirror.Application.Services.Repository.Progresses;
using Mirror.Domain.Entities;

namespace Mirror.Infrastructure.Services.Repository.Progress
{
    public class ProgressRepository(MirrorContext context, ILogger<ProgressRepository> logger) : IProgressRepository
    {
        private readonly MirrorContext _context = context;
        private readonly ILogger<ProgressRepository> _logger = logger;

        public async Task<Mirror.Domain.Entities.Progress> CreateProgressAsync(Mirror.Domain.Entities.Progress progress)
        {
            await _context.Progresses.AddAsync(progress);

            await _context.SaveChangesAsync();

            return progress;
        }

        public async Task<List<Mirror.Domain.Entities.Progress>> GetProgressesAsync()
        {
            var progresses = await _context.Progresses
                .AsNoTracking()
                .Include(p => p.ProgressValue)
                .ToListAsync();

            if (progresses is null)
            {
                return [];
            }

            return progresses;
        }

        public async Task<Domain.Entities.Progress> GetProgressesByIdAsync(Guid progressId)
        {
            var retrievedProgress = await _context.Progresses
                .AsNoTracking()
                .Include(p => p.ProgressValue)
                .FirstOrDefaultAsync(p => p.Id == progressId);

            if (retrievedProgress is null || retrievedProgress.ProgressValue.Count == 0)
            {
                return new();
            }

            return retrievedProgress;
        }

        public async Task<List<Mirror.Domain.Entities.Progress>> GetProgressesByUserAsync(Guid userId)
        {
            return await _context.Progresses
                .AsNoTracking()
                .Where(p => p.UserId == userId)
                .Include(p => p.ProgressValue)
                .ToListAsync();
        }

        public async Task<bool> UpdateProgress(Domain.Entities.Progress existingProgress, Domain.Entities.Progress newProgress)
        {
            if (_context.Entry(existingProgress).State == EntityState.Detached)
            {
                _context.Progresses.Attach(existingProgress);
            }

            // Nastavit stav na Modified, aby EF Core věděl, že se jedná o aktualizaci
            _context.Entry(existingProgress).CurrentValues.SetValues(newProgress);
            _context.Entry(existingProgress).State = EntityState.Modified;

            // Zkontroluj, zda existují nějaké změny v entitě
            var entry = _context.Entry(existingProgress);
            if (entry.State == EntityState.Unchanged)
            {
                _logger.LogWarning($"No changes detected in {nameof(UpdateProgress)}.");
                return false;
            }

            int success = await _context.SaveChangesAsync();

            if (success == 0)
            {
                _logger.LogWarning($"No changes in {nameof(UpdateProgress)} were saved to the database");
                return false;
            }

            return true;
        }
    }
}
