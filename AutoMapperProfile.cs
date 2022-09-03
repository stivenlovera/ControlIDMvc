using AutoMapper;
using ControlIDMvc.Dtos;
using ControlIDMvc.Dtos.Tarjeta;
using ControlIDMvc.Entities;

namespace ControlIDMvc
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PersonaCreateDto, Persona>();
            CreateMap<Persona, PersonaDto>();
            
            CreateMap<TarjetaCreateDto, Tarjeta>();
            CreateMap<Tarjeta, TarjetaDto>();
        }

    }
}