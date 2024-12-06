using AutoMapper;
using Mirror.Contracts.Request.Progress;
using Mirror.Contracts.Request.ProgressValue;
using Mirror.Contracts.Response.Progress;

namespace Mirror.Infrastructure.Mapper.Progress
{
    public class ProgressMapperProfile : Profile
    {
        public ProgressMapperProfile()
        {
            // FROM object mapp TO DTO object 
            CreateMap<Mirror.Domain.Entities.Progress, ProgressResponse>();
            CreateMap<Mirror.Domain.Entities.ProgressValue, ProgressValueDTO>();

            CreateMap<Mirror.Domain.Entities.Progress, CreatedProgressResponse>()
                .ForMember(dest => dest.CreatedProgressId, opt => opt.MapFrom(src => src.Id)); ;

            // FROM DTO to object
            CreateMap<CreateProgressRequest, Mirror.Domain.Entities.Progress>();
            CreateMap<ProgressValueDTO, Mirror.Domain.Entities.ProgressValue>();

        }
    }
}
