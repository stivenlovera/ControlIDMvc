using AutoMapper;
using ControlIDMvc.Dtos;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.DatatableModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class PersonaQuery
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;
        public PersonaQuery(DBContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }
        public async Task<PersonaDto> Show(int id)
        {
            var persona = await _dbContext.Persona.Where(p => p.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<PersonaDto>(persona);
        }
        public async Task<Persona> Store(PersonaCreateDto personaCreateDto)
        {
            var persona = _mapper.Map<Persona>(personaCreateDto);
            var resultado = _dbContext.Persona.Add(persona);
            await _dbContext.SaveChangesAsync();
            return persona;
        }
        public async Task<List<PersonaDto>> GetAll()
        {
            var personas = await this._dbContext.Persona.ToListAsync();
            var resultado = _mapper.Map<List<PersonaDto>>(personas);
            return resultado;
        }

        public async Task<List<Persona>> GetAllByID(List<int> usuarios_id)
        {
            var personas = await this._dbContext.Persona.Where(persona => usuarios_id.Contains(persona.Id)).ToListAsync();
            return personas;
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

            List<PersonaModel> personas = new List<PersonaModel>();
            using (_dbContext)
            {
                personas = (from d in _dbContext.Persona
                            select new PersonaModel
                            {
                                id = d.Id,
                                ci = d.Ci,
                                nombre = d.Nombre,
                                apellido = d.Apellido,
                                celular = d.Celular,
                                observaciones = d.Observaciones
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
        public async Task<Persona> GetOne(int id)
        {
            var persona = await _dbContext.Persona.Where(persona => persona.Id == id).Include(p => p.card).FirstAsync();
            return persona;
        }
        public async Task<bool> ValidarUsuario(string ci)
        {
            System.Console.WriteLine(ci);
            var persona = await _dbContext.Persona.Where(persona => persona.Ci == ci).FirstOrDefaultAsync();

            if (persona == null)
            {
                return true;
            }
            return false;
        }
        public async Task<Persona> EditOne(int id)
        {
            var persona = await _dbContext.Persona.FindAsync(id);
            return persona;
        }
        public async Task<Persona> UpdateOne(Persona persona)
        {
            _dbContext.Update(persona);
            await _dbContext.SaveChangesAsync();
            return persona;
        }
        public async Task<List<PersonaDto>> GetAllLikeCi(int value)
        {
            var personas = await this._dbContext.Persona.Where(p => p.Ci.Contains(value.ToString())).ToListAsync();
            var resultado = _mapper.Map<List<PersonaDto>>(personas);
            return resultado;
        }
        public async Task<List<PersonaDto>> GetAllLikeId(int value)
        {
            var personas = await this._dbContext.Persona.Where(p => p.Id.ToString().Contains(value.ToString())).ToListAsync();
            var resultado = _mapper.Map<List<PersonaDto>>(personas);
            return resultado;
        }
    }
}