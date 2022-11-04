using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.ReglaAcceso;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.DatatableModel;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class ReglaAccesoQuery
    {
        private readonly IMapper _mapper;
        private readonly DBContext _dbContext;

        public ReglaAccesoQuery(
            IMapper mapper,
            DBContext dbContext
            )
        {
            this._mapper = mapper;
            this._dbContext = dbContext;
        }

        /* Datatable */
        public string draw;
        public string start;
        public string length;
        public string showColumn;
        public string showColumnDir;
        public string searchValue;
        public int pageSize, skip, recordsTotal;

        public async Task<object> DataTable(HttpRequest httpRequest)
        {
            var draw = httpRequest.Form["draw"].FirstOrDefault();
            var start = httpRequest.Form["start"].FirstOrDefault();
            var length = httpRequest.Form["length"].FirstOrDefault();
            var sortColumna = httpRequest.Form["column[" + httpRequest.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnaDir = httpRequest.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = httpRequest.Form["search[value]"].FirstOrDefault();

            pageSize = length != null ? Convert.ToInt32(length) : 0;
            skip = start != null ? Convert.ToInt32(start) : 0;
            recordsTotal = 0;

            List<DatatableReglasAcceso> reglasAcceso = new List<DatatableReglasAcceso>();
            using (_dbContext)
            {
                reglasAcceso = await (from a in _dbContext.ReglaAcceso
                                      select new DatatableReglasAcceso
                                      {
                                          id = a.Id,
                                          nombre = a.Nombre,
                                          descripcion = a.Descripcion
                                      }).ToListAsync();
                recordsTotal = reglasAcceso.Count();
                reglasAcceso = reglasAcceso.Skip(skip).Take(pageSize).ToList();
                System.Console.WriteLine($"el total de registros es : {reglasAcceso.Count()}");
            }
            return new
            {
                draw = "draw",
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = reglasAcceso
            };
        }
        public async Task<ReglaAcceso> SearchControlId(int ControlId)
        {
            return await _dbContext.ReglaAcceso.Where(p => p.ControlId == ControlId).FirstOrDefaultAsync();
        }
        public async Task<bool> StoreAll(List<ReglaAcceso> reglaAccesos)
        {
            await _dbContext.AddRangeAsync(reglaAccesos);
            var response = await _dbContext.SaveChangesAsync();
            if (response > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<ReglaAcceso> GetOne(int reglaAccesoId)
        {
            var reglasAcceso = await _dbContext.ReglaAcceso.Where(ra => ra.Id == reglaAccesoId).FirstOrDefaultAsync();
            return reglasAcceso;
        }
        public async Task<ReglaAcceso> Store(ReglaAcceso reglaAcceso)
        {
            await _dbContext.AddRangeAsync(reglaAcceso);
            await _dbContext.SaveChangesAsync();
            return reglaAcceso;
        }
        public async Task<bool> ValidarNombre(string nombre)
        {
            var persona = await _dbContext.Persona.Where(persona => persona.Nombre == nombre).FirstOrDefaultAsync();

            if (persona == null)
            {
                return true;
            }
            return false;
        }
        /*datos ocupados*/
        public async Task<List<Persona>> GetOcupadasPersonaReglaAccesoId(int reglaAccesoId)
        {
            var personasReglasAcceso = await this._dbContext.PersonaReglasAcceso.Where(pr => pr.ReglaAccesoId == reglaAccesoId).Include(x => x.Persona).ToListAsync();
            var personas = new List<Persona>();
            foreach (var persona in personasReglasAcceso)
            {
                personas.Add(persona.Persona);
            }
            return personas;
        }
        public async Task<List<Horario>> GetOcupadasHorarioReglaAccesoId(int reglaAccesoId)
        {
            var horarioReglasAcceso = await this._dbContext.HorarioReglaAcceso.Where(hra => hra.ReglasAccesoId == reglaAccesoId).Include(x => x.Horario).ToListAsync();
            var horarios = new List<Horario>();
            foreach (var horario in horarioReglasAcceso)
            {
                horarios.Add(horario.Horario);
            }
            return horarios;
        }
        public async Task<List<Area>> GetOcupadasAreaReglaAccesoId(int reglaAccesoId)
        {
            var areasReglasAcceso = await this._dbContext.AreaReglaAcceso.Where(ara => ara.ReglaAccesoId == reglaAccesoId).Include(x => x.Area).ToListAsync();
            var areas = new List<Area>();
            foreach (var area in areasReglasAcceso)
            {
                areas.Add(area.Area);
            }
            return areas;
        }
        /*datos disponibles*/
        public async Task<List<Persona>> GetDisponiblePersonaReglaAccesoId(int reglaAccesoId)
        {
            var personasReglasAcceso = await this._dbContext.PersonaReglasAcceso.Where(pr => pr.ReglaAccesoId != reglaAccesoId).Include(x => x.Persona).ToListAsync();
            var personas = new List<Persona>();
            foreach (var persona in personasReglasAcceso)
            {
                personas.Add(persona.Persona);
            }
            return personas;
        }
        public async Task<List<Horario>> GetDisponibleHorarioReglaAccesoId(int reglaAccesoId)
        {
            var horarioReglasAcceso = await this._dbContext.HorarioReglaAcceso.Where(hra => hra.ReglasAccesoId == reglaAccesoId).Include(x => x.Horario).ToListAsync();
            var horarios = new List<Horario>();
            foreach (var horario in horarioReglasAcceso)
            {
                horarios.Add(horario.Horario);
            }
            return horarios;
        }
        public async Task<List<Area>> GetDisponibleAreaReglaAccesoId(int reglaAccesoId)
        {
            var areasReglasAcceso = await this._dbContext.AreaReglaAcceso.Where(ara => ara.ReglaAccesoId != reglaAccesoId).Include(x => x.Area).ToListAsync();
            var areas = new List<Area>();
            foreach (var area in areasReglasAcceso)
            {
                areas.Add(area.Area);
            }
            return areas;
        }
    }
}