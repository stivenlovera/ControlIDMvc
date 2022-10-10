using AutoMapper;
using ControlIDMvc.Dtos;
using ControlIDMvc.Dtos.Accion;
using ControlIDMvc.Dtos.Area;
using ControlIDMvc.Dtos.AreaReglaAccesoCreateDto;
using ControlIDMvc.Dtos.Dispositivo;
using ControlIDMvc.Dtos.HorarioReglaAcceso;
using ControlIDMvc.Dtos.Inscripcion;
using ControlIDMvc.Dtos.Paquete;
using ControlIDMvc.Dtos.PersonaReglasAcceso;
using ControlIDMvc.Dtos.Portal;
using ControlIDMvc.Dtos.PortalReglaAcceso;
using ControlIDMvc.Dtos.ReglaAcceso;
using ControlIDMvc.Dtos.Rol;
using ControlIDMvc.Dtos.RolUsuario;
using ControlIDMvc.Dtos.Tarjeta;
using ControlIDMvc.Dtos.Usuario;
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

            CreateMap<PersonaAccesoReglasCreateDto, PersonaReglasAcceso>();
            CreateMap<PersonaReglasAcceso, PersonaAccesoReglasDto>();

            CreateMap<ReglaAccesoCreateDto, ReglaAcceso>();
            CreateMap<ReglaAcceso, ReglaAccesoDto>();

            CreateMap<TarjetaCreateDto, Tarjeta>();
            CreateMap<Tarjeta, TarjetaDto>();

            CreateMap<AreaCreateDto, Area>();
            CreateMap<Area, AreaDto>();

            CreateMap<AreaReglaAccesoCreateDto, AreaReglaAcceso>();
            CreateMap<AreaReglaAcceso, AreaReglaAccesoDto>();

            CreateMap<HorarioReglaAccesoCreateDto, HorarioReglaAcceso>();
            CreateMap<HorarioReglaAcceso, HorarioReglaAccesoDto>();

            CreateMap<PortalReglaAccesoCreateDto, PortalReglaAcceso>();
            CreateMap<PortalReglaAcceso, PortalReglasAccesoDto>();

            CreateMap<DispositivoCreateDto, Dispositivo>();
            CreateMap<Dispositivo, DispositivoDto>();

            CreateMap<PortalCreateDto, Portal>();
            CreateMap<Portal, PortalDto>();

            CreateMap<AccionCreateDto, Accion>();
            CreateMap<Accion, AccionDto>();

            CreateMap<PaqueteCreateDto, Paquete>();
            CreateMap<Paquete, PaqueteDto>();

            CreateMap<InscripcionCreateDto, Inscripcion>();
            CreateMap<Inscripcion, InscripcionDto>();

            CreateMap<RolCreateDto, Rol>();
            CreateMap<Rol, RolDto>();

            CreateMap<RolUsuarioCreateDto, RolUsuario>();
            CreateMap<RolUsuario, RolUsuarioDto>();
            
            CreateMap<UsuarioCreateDto, Usuario>();
            CreateMap<Usuario, UsuarioDto>();


        }

    }
}