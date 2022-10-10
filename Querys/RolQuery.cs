using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.Rol;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.DatatableModel;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class RolQuery
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;

        public RolQuery(DBContext dbContext, IMapper mapper)
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

            List<DataTableRol> rols = new List<DataTableRol>();
            using (_dbContext)
            {
                rols = (from R in _dbContext.Rol
                        select new DataTableRol
                        {
                            Id = R.Id,
                            Descripcion = R.Nombre,
                            Nombre = R.Nombre
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

        public async Task<List<RolDto>> GetAll()
        {
            var roles = await this._dbContext.Rol.ToListAsync();
            return _mapper.Map<List<RolDto>>(roles);

        }
        public async Task<List<RolDto>> GetAllLike(string value)
        {
            var roles = await this._dbContext.Rol.Where(p => p.Nombre.Contains(value)).ToListAsync();
            return _mapper.Map<List<RolDto>>(roles);
        }
        public async Task<RolDto> Store(RolCreateDto rolCreateDto)
        {
            var rol = _mapper.Map<Rol>(rolCreateDto);
            await _dbContext.AddAsync(rol);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<RolDto>(rol);
        }
        public async Task<RolDto> Update(RolCreateDto rolCreateDto, int id)
        {
            var rol = _mapper.Map<Usuario>(rolCreateDto);
            rol.Id = id;
            _dbContext.Update(rol);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<RolDto>(rol);
        }
        public async Task<bool> Delete(int id)
        {
            var existe = await _dbContext.Rol.AnyAsync(x => x.Id == id);
            if (existe)
            {
                return false;
            }
            _dbContext.Remove(new Rol() { Id = id });
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}