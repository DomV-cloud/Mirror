namespace Mirror.Application.Services.Repository.ProgressSection
{
    public interface IProgressSectionRepository
    {
        public Task<Domain.Entities.ProgressSection> CreateSectionAsync(Domain.Entities.ProgressSection section);

        public Task<bool> DeleteSectionAsync(Domain.Entities.ProgressSection sectionToDelete);
        
        public Task<bool> DeleteSectionsAsync(List<Guid> idsSectionsToDelete);
    }
}
