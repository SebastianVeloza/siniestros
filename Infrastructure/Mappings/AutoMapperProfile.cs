using Application.DTO.Response;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Siniestros, SiniestroResponse>();

        }
    }
}
