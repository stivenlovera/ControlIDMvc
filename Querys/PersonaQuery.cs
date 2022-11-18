using AutoMapper;
using ControlIDMvc.Dtos.Persona;
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
        private long DateTimeToUnix(DateTime MyDateTime)
        {
            TimeSpan timeSpan = MyDateTime - new DateTime(2022, 9, 10, 0, 0, 0);

            return (long)timeSpan.TotalSeconds;
        }
        public async Task<Persona> Show(int id)
        {
            return await _dbContext.Persona.Where(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Persona> Store(Persona persona)
        {
            var resultado = _dbContext.Persona.Add(persona);
            await _dbContext.SaveChangesAsync();
            return persona;
        }
        public async Task<List<Persona>> GetAll()
        {
            return await this._dbContext.Persona.ToListAsync();
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
        public async Task<bool> ValidateExistExceptoId(string ci, int id)
        {
            System.Console.WriteLine(ci);
            var persona = await _dbContext.Persona.Where(persona => persona.Ci == ci).Where(persona => persona.Id != id).FirstOrDefaultAsync();

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
            _dbContext.Entry(await _dbContext.Persona.FirstOrDefaultAsync(x => x.Id == persona.Id)).CurrentValues.SetValues(new
            {
                Id = persona.Id,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Ci = persona.Ci,
                Fecha_nac = persona.Fecha_nac,
                Email = persona.Email,
                Celular = persona.Celular,
                Dirrecion = persona.Dirrecion,
                Observaciones = persona.Observaciones,
                ControlIdName = persona.Nombre,
                ControlIdPassword = persona.ControlIdPassword,
                ControlIdBegin_time = persona.ControlIdBegin_time,
                ControlIdEnd_time = persona.ControlIdEnd_time
            });
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Persona.Where(p => p.Id == persona.Id).Include(x => x.card).FirstAsync();
        }
        public async Task<Persona> UpdatefechaValidez(Persona persona)
        {
            _dbContext.Entry(await _dbContext.Persona.FirstOrDefaultAsync(x => x.Id == persona.Id)).CurrentValues.SetValues(new
            {
                Id = persona.Id,
                ControlIdBegin_time = persona.ControlIdBegin_time,
                ControlIdEnd_time = persona.ControlIdEnd_time
            });
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Persona.Where(p => p.Id == persona.Id).Include(x => x.card).FirstAsync();
        }
        public async Task<List<Persona>> GetAllLikeCi(int value)
        {
            return await this._dbContext.Persona.Where(p => p.Ci.Contains(value.ToString())).ToListAsync();
        }
        public async Task<List<Persona>> GetAllLikeId(int value)
        {
            var personas = await this._dbContext.Persona.Where(p => p.Id.ToString().Contains(value.ToString())).ToListAsync();
            return personas;
        }
        public async Task<bool> Delete(int id)
        {
            var persona = await _dbContext.Persona.Where(x => x.Id == id).FirstAsync();
            if (persona != null)
            {
                _dbContext.Persona.Remove(persona);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<Persona>> GetAllReglaAccesoId(int reglaAccesoId)
        {
            var personas = await this._dbContext.ReglaAcceso.Where(ra => ra.Id == reglaAccesoId).Include(x => x.PersonaReglasAcceso).ToListAsync();
            return await this._dbContext.Persona.ToListAsync();
        }
    }
}