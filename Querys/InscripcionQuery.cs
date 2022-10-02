using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.Inscripcion;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.DatatableModel;

namespace ControlIDMvc.Querys
{
    public class InscripcionQuery
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;
        public InscripcionQuery(DBContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        /* Datatable */
        public string draw;
        public string start;
        public string length;
        public string showColumn;
        public string showColumnDir;
        public string searchValue;
        public int pageSize, skip, recordsTotal;

        public object DataTable(HttpRequest httpRequest)
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

            List<DatatableInscripciones> personas = new List<DatatableInscripciones>();
            using (_dbContext)
            {
                personas = (from i in _dbContext.Inscripcion
                            select new DatatableInscripciones
                            {
                                Id = i.Id,
                                PaqueteNombre = i.Paquete.Nombre,
                                PersonaNombre = i.Persona.Nombre,
                                PersonaCi = i.Persona.Ci,
                                PaqueteId = i.PaqueteId,
                                PersonaId = i.PersonaId,
                                PaqueteCosto = i.Paquete.Costo,
                                PaqueteDias = i.Paquete.Dias,
                                FechaCreacion = i.FechaCreacion.ToString("dddd, dd MMMM yyyy"),
                                FechaFin = i.FechaFin,
                                FechaInicio = i.FechaFin
                            }).ToList();
                recordsTotal = personas.Count();
                personas = personas.Skip(skip).Take(pageSize).ToList();
                System.Console.WriteLine($"el total de registros es : {personas.Count()}");
            }
            return new
            {
                draw = "draw",
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = personas
            };
        }
        public async Task<InscripcionDto> Store(InscripcionCreateDto inscripcionCreateDto)
        {
            var inscripcion = _mapper.Map<Inscripcion>(inscripcionCreateDto);
            _dbContext.Inscripcion.Add(inscripcion);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<InscripcionDto>(inscripcion);
        }
    }
}