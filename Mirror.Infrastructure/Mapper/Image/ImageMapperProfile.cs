using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mirror.Contracts.Request.Images.GET;
using Mirror.Contracts.Request.Memory.POST;
using Mirror.Contracts.Response.Memory;
using Mirror.Domain.Entities;

namespace Mirror.Infrastructure.Mapper.Image
{
    public class ImageMapperProfile : Profile
    {
        public ImageMapperProfile()
        {
            CreateMap<IFormFile, Domain.Entities.Image>();

            CreateMap<Domain.Entities.Image, ImageResponse>();
        }
    }
}
