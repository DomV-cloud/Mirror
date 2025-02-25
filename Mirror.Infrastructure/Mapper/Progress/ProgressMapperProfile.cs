using Mirror.Contracts.Request.Progress.POST;
using Mirror.Contracts.Request.Progress.PUT;
using Mirror.Contracts.Response.Progress;
using Mirror.Contracts.Response.ProgressValue;
using Mirror.Infrastructure.Mapper.Common;

namespace Mirror.Infrastructure.Mapper.Progress
{
    public class ProgressMapperProfile : CommonMapperProfile
    {
        public ProgressMapperProfile()
        {
            // FIRST OBJECT IS INPUT, SECOND OUTPUT 
            CreateMap<Domain.Entities.Progress, ProgressResponse>()
            .ForMember(dest => dest.CreatedProgressId, opt => opt.MapFrom(src => src.Id));

            CreateMap<CreateProgressRequest, Mirror.Domain.Entities.Progress>();

            CreateMap<ProgressValueResponse, Mirror.Domain.Entities.ProgressValue>();

            CreateMap<UpdateProgressRequest, Mirror.Domain.Entities.Progress>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => src.NewSections));
        }
    }
}
