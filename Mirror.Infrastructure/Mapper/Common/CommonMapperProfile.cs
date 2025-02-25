using AutoMapper;
using Mirror.Contracts.Request.ProgressSection.POST;
using Mirror.Contracts.Request.ProgressValue.POST;
using Mirror.Contracts.Response.ProgressValue;
using Mirror.Contracts.Response.Section;
using Mirror.Domain.Entities;

namespace Mirror.Infrastructure.Mapper.Common
{
    public class CommonMapperProfile : Profile
    {
        public CommonMapperProfile()
        {
            CreateMap<Mirror.Domain.Entities.ProgressValue, ProgressValueResponse>();
            CreateMap<ProgressValueRequest, Mirror.Domain.Entities.ProgressValue>();

            CreateMap<ProgressSection, ProgressSectionResponse>();
            CreateMap<CreateProgressSectionRequest, ProgressSection>();

            CreateMap<ProgressSectionResponse, ProgressSection>();
        }
    }
}
