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

        public async Task<Domain.Entities.Progress> GetActiveProgressByUserAsync(Guid userId)
        {
            var activeProgress = await _context.Progresses
                .AsNoTracking() 
                .AsSplitQuery() //.AsSplitQuery() I would considering, if in this case is good to use. 
                .Where(p => p.UserId == userId)
                .Include(p => p.Sections)
                .ThenInclude(p => p.ProgressValues)
                .FirstOrDefaultAsync(p => p.IsActive);

            if (activeProgress == null)
            {
                return new();
            }

            return activeProgress;
        }

        public async Task<List<Mirror.Domain.Entities.Progress>> GetProgressesAsync()
        {
            var progresses = await _context.Progresses
                .AsNoTracking()
                .AsSplitQuery()
                .Include(p => p.Sections)
                .ThenInclude(p => p.ProgressValues)
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
                .AsSplitQuery()
                .Include(p => p.Sections)
                .ThenInclude(p => p.ProgressValues)
                .FirstOrDefaultAsync(p => p.Id == progressId);

            if (retrievedProgress is null)
            {
                return new();
            }

            return retrievedProgress;
        }

        public async Task<List<Mirror.Domain.Entities.Progress>> GetProgressesByUserAsync(Guid userId)
        {
            return await _context.Progresses
                .AsNoTracking()
                .AsSplitQuery()
                .Where(p => p.UserId == userId)
                .Include(p => p.Sections)
                .ThenInclude(p => p.ProgressValues)
                .OrderByDescending(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<bool> UpdateProgressAsync(Domain.Entities.Progress existingProgress, Domain.Entities.Progress newProgress)
        {
            if (existingProgress.Id == newProgress.Id)
            {
                return false;
            }

            existingProgress.ProgressName = newProgress.ProgressName;
            existingProgress.Description = newProgress.Description;
            existingProgress.IsActive = newProgress.IsActive;
            existingProgress.Sections = newProgress.Sections;

            _context.Progresses.Update(existingProgress);

            var entry = _context.Entry(existingProgress);
            if (entry.State == EntityState.Unchanged)
            {
                _logger.LogWarning($"No changes detected in {nameof(UpdateProgressAsync)}.");
                return false;
            }

            int success = await _context.SaveChangesAsync();

            if (success == 0)
            {
                _logger.LogWarning($"No changes in {nameof(UpdateProgressAsync)} were saved to the database");
                return false;
            }

            return true;
        }
    }
}
