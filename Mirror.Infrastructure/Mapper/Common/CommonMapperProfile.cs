using AutoMapper;
using Mirror.Contracts.Request.ProgressValue;
using Mirror.Contracts.Response.Section;
using Mirror.Domain.Entities;

namespace Mirror.Infrastructure.Mapper.Common
{
    public class CommonMapperProfile : Profile
    {
        public CommonMapperProfile() {
            CreateMap<Mirror.Domain.Entities.ProgressValue, ProgressValueResponse>();
            CreateMap<ProgressSection, ProgressSectionResponse>();
        }
    }
}
