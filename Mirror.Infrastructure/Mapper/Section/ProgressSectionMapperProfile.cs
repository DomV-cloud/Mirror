using AutoMapper;
using Mirror.Contracts.Request.ProgressValue;
using Mirror.Contracts.Response.Section;
using Mirror.Domain.Entities;

namespace Mirror.Infrastructure.Mapper.Section
{
    public class ProgressSectionMapperProfile : Profile
    {
        public ProgressSectionMapperProfile()
        {
            CreateMap<Domain.Entities.ProgressValue, ProgressValueResponse>();
            CreateMap<ProgressSection, ProgressSectionResponse>();
        }
    }
}
