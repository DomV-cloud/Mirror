using Microsoft.EntityFrameworkCore;
using Mirror.Application.DatabaseContext;
using Mirror.Application.Services.Repository.Progresses;

namespace Mirror.Infrastructure.Services.Repository.Progress
{
    public class ProgressRepository(MirrorContext context) : IProgressRepository
    {
        private readonly MirrorContext _context = context;

        public async Task<Mirror.Domain.Entities.Progress> CreateProgress(Mirror.Domain.Entities.Progress progress)
        {
            await _context.Progresses.AddAsync(progress);

            await _context.SaveChangesAsync();

            return progress;
        }

        public async Task<List<Mirror.Domain.Entities.Progress>> GetProgressesAsync()
        {
            return await _context.Progresses
                  .Include(p => p.ProgressValue)
                  .ToListAsync();
        }

        public async Task<List<Mirror.Domain.Entities.Progress>> GetProgressesByUserAsync(Guid userId)
        {
            return await _context.Progresses
                .AsNoTracking()
                .Where(p => p.UserId == userId)
                .Include(p => p.ProgressValue)
                .ToListAsync();
        }
    }
}
