using Microsoft.AspNetCore.Http;
using Mirror.Domain.Entities;

namespace Mirror.Application.Services.Repository.Image
{
    public interface IImageRepository
    {
        public Task<Domain.Entities.Image> CreateImageAsync(IFormFile file);

        public Task DeleteImageAsync(List<Guid> imageIdsToDelete);
    }
}
