using AutoMapper;
using Mirror.Contracts.Request.Memory.POST;
using Mirror.Contracts.Request.Memory.PUT;
using Mirror.Contracts.Response.Memory;
using Mirror.Domain.Entities;

namespace Mirror.Infrastructure.Mapper.Memory
{
    public class UserMemoryMapperProfile : Profile
    {
        public UserMemoryMapperProfile()
        {
            CreateMap<UserMemoryCreateRequest, UserMemory>();
            CreateMap<UserMemory, UserMemoryResponse>()
                .ForMember(dest => dest.MemoryId, opt => opt.MapFrom(src => src.Id));

            CreateMap<UserMemoryUpdateRequest, UserMemory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.NewImages));
        }
    }
}
