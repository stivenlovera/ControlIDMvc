using ControlIDMvc.Entities;
using ControlIDMvc.Models.DatatableModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class PersonaQuery
    {
        private readonly DBContext _dbContext;
        public PersonaQuery(DBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<bool> Store(Persona personaCreate)
        {
            _dbContext.Persona.Add(personaCreate);
            var resultado = await _dbContext.SaveChangesAsync();
            if (resultado == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<List<Persona>> GetAll()
        {
            var personas = await this._dbContext.Persona.ToListAsync();
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

        [HttpPost("data-table")]
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
                                id = d.id,
                                ci = d.ci,
                                nombre = d.nombre,
                                apellido = d.apellido,
                                celular = d.celular,
                                observaciones = d.observaciones
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
            var persona =await  _dbContext.Persona.FindAsync(id);
            return persona;
        }
    }
}