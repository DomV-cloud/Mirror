using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mirror.Application.DatabaseContext;
using Mirror.Application.Services.Repository.ProgressValues;

namespace Mirror.Infrastructure.Services.Repository.ProgressValues
{
    public class ProgressValueRepository : IProgressValueRepository
    {
        private readonly MirrorContext _context;
        private readonly ILogger<ProgressValueRepository> _logger;

        public ProgressValueRepository(MirrorContext context, ILogger<ProgressValueRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Domain.Entities.ProgressValue>> GetProgressValueByProgressAsync(Guid progressId)
        {
            _logger.LogInformation("Fetching progress values for ProgressId: {ProgressId}", progressId);

            var progressValues = await _context.ProgressValues
                .AsNoTracking()
                .Where(pv => pv.ProgressId == progressId)
                .ToListAsync();

            if (!progressValues.Any())
            {
                _logger.LogWarning("No progress values found for ProgressId: {ProgressId}", progressId);
            }
            else
            {
                _logger.LogInformation("Retrieved {Count} progress values for ProgressId: {ProgressId}", progressValues.Count, progressId);
            }

            return progressValues;
        }

        public async Task<List<Domain.Entities.ProgressValue>> GetProgressValuesAsync()
        {
            _logger.LogInformation("Fetching all progress values.");

            var progressValues = await _context.ProgressValues
                .AsNoTracking()
                .ToListAsync();

            if (!progressValues.Any())
            {
                _logger.LogWarning("No progress values found.");
            }
            else
            {
                _logger.LogInformation("Retrieved {Count} progress values.", progressValues.Count);
            }

            return progressValues;
        }
    }
}