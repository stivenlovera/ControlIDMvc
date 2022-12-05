using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.Inscripcion;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.DatatableModel;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class InscripcionQuery
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;
        public InscripcionQuery(
            DBContext dbContext,
            IMapper mapper
            )
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public object DataTable(HttpRequest httpRequest)
        {
            int totalRecord = 0;
            int filterRecord = 0;

            var draw = httpRequest.Form["draw"].FirstOrDefault();
            var sortColumn = httpRequest.Form["columns[" + httpRequest.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = httpRequest.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = httpRequest.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(httpRequest.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(httpRequest.Form["start"].FirstOrDefault() ?? "0");

            var data = (from i in _dbContext.Inscripcion
                        select new DatatableInscripciones()
                        {
                            Id = i.Id,
                            PaqueteNombre = i.Paquete.Nombre,
                            numeroRecibo = i.NumeroRecibo,
                            PersonaNombre = i.Persona.Nombre,
                            PersonaCi = i.Persona.Ci,
                            PaqueteId = i.PaqueteId,
                            PersonaId = i.PersonaId,
                            PaqueteCosto = i.Paquete.Costo,
                            PaqueteDias = i.Paquete.Dias,
                            FechaCreacion = i.FechaCreacion.ToString("dddd, dd MMMM yyyy"),
                            FechaFin = i.FechaFin,
                            FechaInicio = i.FechaFin
                        });

            totalRecord = data.Count();
            // buscar por valor
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x => x.PaqueteNombre.ToLower().Contains(searchValue.ToLower()) || x.PersonaNombre.ToLower().Contains(searchValue.ToLower()) || x.PersonaCi.ToLower().Contains(searchValue.ToLower()) || x.PaqueteCosto.ToString().ToLower().Contains(searchValue.ToLower()));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //filtro columna
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection)) data = data.OrderBy(x => sortColumn).ThenBy(x => sortColumnDirection);
            //pagination
            var empList = data.Skip(skip).Take(pageSize).ToList();
            return new
            {
                draw = draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = empList
            };
        }
        public async Task<Inscripcion> Store(Inscripcion Inscripcion)
        {
            _dbContext.Inscripcion.Add(Inscripcion);
            await _dbContext.SaveChangesAsync();
            return Inscripcion;
        }
        public async Task<List<Inscripcion>> GetAllByPersonaId(int PersonaId)
        {
            return await _dbContext.Inscripcion.Where(x => x.PersonaId==PersonaId).ToListAsync();
        }

        public async Task<Inscripcion> Edit(int id)
        {
            var inscripcion = await _dbContext.Inscripcion.Where(i => i.Id == id).Include(p => p.Persona).Include(p => p.Paquete).FirstOrDefaultAsync();
            return inscripcion;
        }

        public async Task<Inscripcion> Update(Inscripcion inscripcion, int id)
        {
            _dbContext.Entry(await _dbContext.Inscripcion.FirstOrDefaultAsync(x => x.Id == id)).CurrentValues.SetValues(new
            {
                Id = inscripcion.Id,
                FechaInicio = inscripcion.FechaInicio,
                FechaFin = inscripcion.FechaFin,
                Costo = inscripcion.Costo,
                PersonaId = inscripcion.PersonaId,
                PaqueteId = inscripcion.PaqueteId
            });
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Inscripcion.Where(p => p.Id == id).FirstAsync();
        }
        public async Task<bool> Delete(int id)
        {
            var existe = await _dbContext.Inscripcion.AnyAsync(x => x.Id == id);
            if (existe)
            {
                _dbContext.Remove(new Inscripcion() { Id = id });
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}