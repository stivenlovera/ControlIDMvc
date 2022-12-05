using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.Usuario;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.DatatableModel;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class UsuarioQuery
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;
        public UsuarioQuery(DBContext dbContext, IMapper mapper)
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

            List<DataTableUsuario> usuarios = new List<DataTableUsuario>();
            using (_dbContext)
            {
                usuarios = (from u in _dbContext.Usuario
                            select new DataTableUsuario
                            {
                                Id = u.Id,
                                Nombres = u.Persona.Nombre,
                                Apellidos = u.Persona.Apellido,
                                Password = u.Password,
                                PersonaId = u.PersonaId,
                                User = u.User,

                            }).ToList();
                recordsTotal = usuarios.Count();
                usuarios = usuarios.Skip(skip).Take(pageSize).ToList();
                System.Console.WriteLine($"el total de registros es : {usuarios.Count()}");
            }
            return new
            {
                draw = "draw",
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = usuarios
            };
        }
        public async Task<Usuario> Login(string user, string password)
        {
           return await this._dbContext.Usuario.Where(u => u.User == user).Where(u => u.Password == password).Include(x => x.Persona).ThenInclude(x=>x.perfil).FirstOrDefaultAsync();
        }
        public async Task<List<UsuarioDto>> GetAll()
        {
            var usuarios = await this._dbContext.Usuario.ToListAsync();
            return _mapper.Map<List<UsuarioDto>>(usuarios);

        }
        public async Task<List<UsuarioDto>> GetAllLike(string value)
        {
            var usuarios = await this._dbContext.Usuario.Where(p => p.User.Contains(value)).ToListAsync();
            return _mapper.Map<List<UsuarioDto>>(usuarios);
        }
        public async Task<UsuarioDto> Store(UsuarioCreateDto usuarioCreateDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioCreateDto);
            await _dbContext.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<UsuarioDto>(usuario);
        }
        public async Task<UsuarioDto> ShowPersonaId(int id)
        {
            var persona = await _dbContext.Usuario.Where(u => u.PersonaId == id).FirstOrDefaultAsync();
            return _mapper.Map<UsuarioDto>(persona);
        }
        public async Task<Usuario> Update(Usuario usuario)
        {
           _dbContext.Entry(await _dbContext.Usuario.FirstOrDefaultAsync(x => x.Id == usuario.Id)).CurrentValues.SetValues(new
            {
                Id=usuario.Id,
                User=usuario.User,
                Password=usuario.Password
            });
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Usuario.Where(p => p.Id == usuario.Id).FirstAsync();
        }
        public async Task<UsuarioDto> ValidatePersonaId(int id)
        {
            var usuario = await _dbContext.Usuario.Where(x => x.PersonaId == id).FirstOrDefaultAsync();
            return _mapper.Map<UsuarioDto>(usuario);
        }
        public async Task<bool> Delete(int id)
        {
            var existe = await _dbContext.Usuario.AnyAsync(x => x.Id == id);
            if (existe)
            {
                return false;
            }
            _dbContext.Remove(new Usuario() { Id = id });
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}