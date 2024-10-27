using Microsoft.EntityFrameworkCore;
using Mirror.Application.DatabaseContext;
using Mirror.Application.Services.Repository.ProgressValues;

namespace Mirror.Infrastructure.Services.Repository.ProgressValues
{
    public class ProgressValueRepository(MirrorContext context) : IProgressValueRepository
    {
        private readonly MirrorContext _context = context;

        public async Task<List<Domain.Entities.ProgressValue>> GetProgressValueByProgressAsync(Guid progressId)
        {
            return await _context.ProgressValues
                .AsNoTracking()
                .Where(pv => pv.ProgressId == progressId)
                .ToListAsync();
        }

        public async Task<List<Domain.Entities.ProgressValue>> GetProgressValuesAsync()
        {
            return await _context.ProgressValues.ToListAsync();
        }
    }
}
