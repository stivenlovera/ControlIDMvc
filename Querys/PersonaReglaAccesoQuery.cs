using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.PersonaReglasAcceso;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class PersonaReglaAccesoQuery
    {
        private readonly IMapper _mapper;
        private readonly DBContext _dbContext;

        public PersonaReglaAccesoQuery(
            IMapper mapper,
            DBContext dbContext
            )
        {
            this._mapper = mapper;
            this._dbContext = dbContext;
        }
        public async Task<PersonaReglasAcceso> Store(PersonaReglasAcceso personaReglasAcceso)
        {
            var resultado = _dbContext.PersonaReglasAcceso.Add(personaReglasAcceso);
            await _dbContext.SaveChangesAsync();
            return personaReglasAcceso;
        }
        public async Task<List<PersonaReglasAcceso>> GetAllReglaAccesoId(int reglaAccesoId)
        {
            return await _dbContext.PersonaReglasAcceso.Where(pr => pr.ReglaAccesoId == reglaAccesoId).ToListAsync();

        }
        public async Task<bool> DeleteAllReglaAccesoId(int ReglaAccesoId)
        {
            var buscar_personas = await this._dbContext.PersonaReglasAcceso.Where(x => x.ReglaAccesoId == ReglaAccesoId).ToListAsync();
            _dbContext.PersonaReglasAcceso.RemoveRange(buscar_personas);
            var resultado = await _dbContext.SaveChangesAsync();
            if (resultado > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<List<PersonaReglasAcceso>> StoreAll(List<PersonaReglasAcceso> personaReglasAccesos)
        {
            await _dbContext.PersonaReglasAcceso.AddRangeAsync(personaReglasAccesos);
            await _dbContext.SaveChangesAsync();
            var ids = new List<int>();
            foreach (var personaReglasAcceso in personaReglasAccesos)
            {
                ids.Add(personaReglasAcceso.Id);
            }
            return await _dbContext.PersonaReglasAcceso.Where(pr => ids.Contains(pr.Id)).Include(x => x.Persona).ToListAsync();
        }
        public async Task<PersonaReglasAcceso> UpdateControlId(PersonaReglasAcceso personaReglasAcceso)
        {
            _dbContext.Entry(await _dbContext.PersonaReglasAcceso.FirstOrDefaultAsync(x => x.PersonaId == personaReglasAcceso.PersonaId && x.ReglaAccesoId == personaReglasAcceso.ReglaAccesoId)).CurrentValues.SetValues(
               new
               {
                   ControlIdAccessRulesId = personaReglasAcceso.ControlIdAccessRulesId,
                   ControlIdUserId = personaReglasAcceso.ControlIdUserId
               });
            await _dbContext.SaveChangesAsync();
            return await _dbContext.PersonaReglasAcceso.Where(x => x.PersonaId == personaReglasAcceso.PersonaId && x.ReglaAccesoId == personaReglasAcceso.ReglaAccesoId).FirstAsync();
        }
    }
}