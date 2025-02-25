using Microsoft.EntityFrameworkCore;
using Mirror.Application.DatabaseContext;
using Mirror.Application.Services.Repository.ProgressSection;

namespace Mirror.Infrastructure.Persistance.Repository.ProgressSection
{
    public class ProgressSectionRepository : IProgressSectionRepository
    {
        private readonly MirrorContext _context;

        public ProgressSectionRepository(MirrorContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.ProgressSection> CreateSectionAsync(Domain.Entities.ProgressSection section)
        {
            if (section == null)
            {
                return new();
            }

            await _context.ProgressSections.AddAsync(section);
            await _context.SaveChangesAsync();

            return section;
        }

        public async Task<bool> DeleteSectionAsync(Domain.Entities.ProgressSection sectionToDelete)
        {
            if (sectionToDelete == null)
            {
                return new();
            }

            _context.ProgressSections.Remove(sectionToDelete);
            var updatedRows = await _context.SaveChangesAsync();

            if (updatedRows < 0)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteSectionsAsync(List<Guid> idsSectionsToDelete)
        {
            if (idsSectionsToDelete == null || idsSectionsToDelete.Count == 0)
            {
                return false;
            }

            foreach (var section in idsSectionsToDelete)
            {
                if (section != Guid.Empty)
                {
                    var sectionToDelete = await _context.ProgressSections.FirstOrDefaultAsync(s => s.Id == section);
                    if (sectionToDelete == null)
                    {
                        return false;
                    }
                    _context.ProgressSections.Remove(sectionToDelete);
                }
            }
            var deletedRows =  await _context.SaveChangesAsync(true);

            if (deletedRows < 0)
            {
                return false;
            }

            return true;
        }
    }
}
