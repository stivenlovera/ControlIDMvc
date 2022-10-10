using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.Rol;
using ControlIDMvc.Dtos.RolUsuario;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.DatatableModel;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class RolesUsuarioQuery
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;

        public RolesUsuarioQuery(DBContext dbContext, IMapper mapper)
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

            List<DataTableRolesUsuario> rols = new List<DataTableRolesUsuario>();
            using (_dbContext)
            {
                rols = (from u in _dbContext.Usuario
                        select new DataTableRolesUsuario
                        {
                            Id = u.Id,
                            Nombres = u.Persona.Nombre,
                            Apellidos = u.Persona.Apellido,
                            Roles = u.RolUsuarios.ToString(),
                            Usuario = u.User,
                            UsuarioId = u.Id
                        }).ToList();
                recordsTotal = rols.Count();
                rols = rols.Skip(skip).Take(pageSize).ToList();
                System.Console.WriteLine($"el total de registros es : {rols.Count()}");
            }
            return new
            {
                draw = "draw",
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = rols
            };
        }

        public async Task<List<RolUsuarioDto>> GetAll()
        {
            var roles = await this._dbContext.RolUsuario.ToListAsync();
            return _mapper.Map<List<RolUsuarioDto>>(roles);

        }
        public async Task<List<string>> GetRoles(int id)
        {
            var rolesUsuario = await this._dbContext.RolUsuario.Where(ru => ru.UsuarioId == id).Include(r => r.Rol).ToListAsync();
            List<string> roles = new List<string>();
            foreach (var rol in rolesUsuario)
            {
                roles.Add(rol.Rol.Nombre);
            }
            return roles;
        }
        public async Task<List<RolDto>> GetRolesId(int id)
        {
            var rolesUsuario = await this._dbContext.RolUsuario.Where(ru => ru.UsuarioId == id).Include(r => r.Rol).ToListAsync();
            List<RolDto> roles=new List<RolDto>();
            foreach (var rol in rolesUsuario)
            {
                roles.Add( _mapper.Map<RolDto>(rol.Rol));
            }
            return roles;
        }
        public async Task<List<RolUsuarioDto>> GetAllLike(string value)
        {
            var roles = await this._dbContext.RolUsuario.Where(p => p.UsuarioId.ToString().Contains(value)).ToListAsync();
            return _mapper.Map<List<RolUsuarioDto>>(roles);
        }
        public async Task<RolUsuarioDto> Store(RolUsuarioCreateDto rolUsuarioCreateDto)
        {
            var rol = _mapper.Map<RolUsuario>(rolUsuarioCreateDto);
            await _dbContext.AddAsync(rol);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<RolUsuarioDto>(rol);
        }
        public async Task<RolUsuarioDto> Update(RolUsuarioCreateDto rolUsuarioCreateDto, int id)
        {
            var rolUsuario = _mapper.Map<RolUsuario>(rolUsuarioCreateDto);
            rolUsuario.Id = id;
            _dbContext.Update(rolUsuario);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<RolUsuarioDto>(rolUsuario);
        }
        public async Task<bool> Delete(int id)
        {
            var existe = await _dbContext.RolUsuario.AnyAsync(x => x.Id == id);
            if (existe)
            {
                return false;
            }
            _dbContext.Remove(new RolUsuario() { Id = id });
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteUsuarioId(int id)
        {
            var existe= await _dbContext.RolUsuario.Where(ru=>ru.UsuarioId==id).ToListAsync();
            if (existe==null)
            {
                 _dbContext.RemoveRange(existe);
                 await _dbContext.SaveChangesAsync();
            }

            return true;
        }
    }
}