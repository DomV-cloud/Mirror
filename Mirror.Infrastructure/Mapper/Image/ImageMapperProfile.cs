using AutoMapper;
using Mirror.Contracts.Request.Memory.POST;
using Mirror.Domain.Entities;

namespace Mirror.Infrastructure.Mapper.Image
{
    public class ImageMapperProfile : Profile
    {
        public ImageMapperProfile()
        {
            CreateMap<UserMemoryCreateRequest, UserMemory>();
        }
    }
}
