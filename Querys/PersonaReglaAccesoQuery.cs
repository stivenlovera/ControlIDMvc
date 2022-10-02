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
        public async Task<PersonaReglasAcceso> Store(PersonaAccesoReglasCreateDto personaAccesoReglasCreateDto)
        {
            var personaReglaAcceso = _mapper.Map<PersonaReglasAcceso>(personaAccesoReglasCreateDto);
            var resultado = _dbContext.PersonaReglasAcceso.Add(personaReglaAcceso);
            await _dbContext.SaveChangesAsync();
            return personaReglaAcceso;
        }
    }
}