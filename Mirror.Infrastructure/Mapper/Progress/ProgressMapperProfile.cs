using AutoMapper;
using Mirror.Contracts.Progress;
using Mirror.Contracts.ProgressValue;

namespace Mirror.Infrastructure.Mapper.Progress
{
    public class ProgressMapperProfile : Profile
    {
        public ProgressMapperProfile()
        {
            // FROM object mapp TO object 
            CreateMap<Mirror.Domain.Entities.Progress, ProgressDTO>();
            CreateMap<Mirror.Domain.Entities.ProgressValue, ProgressValueDTO>();

            CreateMap<CreateProgressDTO, Mirror.Domain.Entities.Progress>();
            CreateMap<ProgressValueDTO, Mirror.Domain.Entities.ProgressValue>();
        }
    }
}
