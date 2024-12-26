using AutoMapper;
using Mirror.Contracts.Request.Progress.POST;
using Mirror.Contracts.Request.Progress.PUT;
using Mirror.Contracts.Request.ProgressValue;
using Mirror.Contracts.Response.Progress;
using Mirror.Domain.Entities;

namespace Mirror.Infrastructure.Mapper.Progress
{
    public class ProgressMapperProfile : Profile
    {
        public ProgressMapperProfile()
        {
            // FROM object mapp TO DTO object 
            CreateMap<Domain.Entities.Progress, ProgressResponse>();
            CreateMap<Mirror.Domain.Entities.ProgressValue, ProgressValueDTO>();

            CreateMap<Domain.Entities.Progress, ProgressResponse>()
            .ForMember(dest => dest.CreatedProgressId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProgressValue, opt => opt.MapFrom((src, dest, destMember, context) =>
                src.ProgressValue
                    .GroupBy(pv => pv.ProgressColumnHead)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(pv => context.Mapper.Map<ProgressValueDTO>(pv)).ToList()
                    )
            ));

            // FROM DTO to object
            CreateMap<CreateProgressRequest, Mirror.Domain.Entities.Progress>();

            CreateMap<ProgressValueDTO, Mirror.Domain.Entities.ProgressValue>();

            CreateMap<UpdateProgressRequest, Mirror.Domain.Entities.Progress>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ProgressValue, opt => opt.MapFrom(src => src.ProgressValues));

            CreateMap<Dictionary<string, List<ProgressValueDTO>>, List<ProgressValue>>()
            .ConvertUsing((src, dest, context) =>
            {
                return src.SelectMany(pair => pair.Value.Select(dto => context.Mapper.Map<ProgressValue>(dto)))
                          .ToList();
            });
        }
    }
}
