using AutoMapper;
using Projekt_strona.Models;
using Projekt_strona.Models.Dto;

namespace Projekt_strona.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDto>();
            CreateMap<CarDto, Car>();
        }
    }
}