using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.PersonaReglasAcceso;
using ControlIDMvc.Entities;

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
        public async Task<List<PersonaReglasAcceso>> StoreAll(List<PersonaReglasAcceso> personaReglasAccesos)
        {
            await  _dbContext.PersonaReglasAcceso.AddRangeAsync(personaReglasAccesos);
            await _dbContext.SaveChangesAsync();
            return personaReglasAccesos;
        }
    }
}