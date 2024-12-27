using AutoMapper;
using Mirror.Contracts.Request.Memory.POST;
using Mirror.Contracts.Response.Memory;
using Mirror.Domain.Entities;

namespace Mirror.Infrastructure.Mapper.Memory
{
    public class UserMemoryMapperProfile : Profile
    {
        public UserMemoryMapperProfile()
        {
            CreateMap<UserMemoryCreateRequest, UserMemory>();
            CreateMap<UserMemory, UserMemoryResponse>();
        }
    }
}
