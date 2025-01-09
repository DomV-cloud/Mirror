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
            if (existingProgress.Id == newProgress.Id)
            {
                return false;
            }

            existingProgress.ProgressName = newProgress.ProgressName;
            existingProgress.Description = newProgress.Description;
            existingProgress.Description = newProgress.Description;
            existingProgress.ProgressValue = newProgress.ProgressValue;
            existingProgress.IsAchieved = newProgress.IsAchieved;
            existingProgress.TrackedDays = newProgress.TrackedDays;
            existingProgress.TrackingProgressDay = newProgress.TrackingProgressDay;
            existingProgress.PercentageAchieved = newProgress.PercentageAchieved;
            existingProgress.Updated = newProgress.Updated;
            UpdateProgressValues(existingProgress, newProgress.ProgressValue);

            _context.Progresses.Update(existingProgress);
            int success = await _context.SaveChangesAsync();

            if (success == 0)
            {
                _logger.LogWarning($"No changes in {nameof(UpdateProgress)} were saved to the database");
                return false;
            }

            return true;
        }

        private void UpdateProgressValues(Domain.Entities.Progress existingProgress, List<Domain.Entities.ProgressValue> newProgressValues)
        {
            existingProgress.ProgressValue.RemoveAll(p => !newProgressValues.Any(newImg => newImg.Id == p.Id));

            foreach (var newImage in newProgressValues)
            {
                if (!existingProgress.ProgressValue.Any(img => img.Id == newImage.Id))
                {
                    existingProgress.ProgressValue.Add(newImage);
                }
            }
        }
    }
}
