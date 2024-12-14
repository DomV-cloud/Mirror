using Microsoft.EntityFrameworkCore;
using Mirror.Application.DatabaseContext;
using Mirror.Application.Services.Repository.Progresses;

namespace Mirror.Infrastructure.Services.Repository.Progress
{
    public class ProgressRepository(MirrorContext context) : IProgressRepository
    {
        private readonly MirrorContext _context = context;

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
            _context.Entry(existingProgress).CurrentValues.SetValues(newProgress);

            int success = await _context.SaveChangesAsync();

            if (success == 0)
            {
                return false;
            }

            return true;
        }
    }
}
