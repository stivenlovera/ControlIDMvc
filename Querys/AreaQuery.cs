using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.Area;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.DatatableModel;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{

    public class AreaQuery
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;
        public AreaQuery(DBContext dbContext, IMapper mapper)
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

        public async Task<object> DataTable(HttpRequest httpRequest)
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

            List<DatatableArea> personas = new List<DatatableArea>();
            using (_dbContext)
            {
                personas = await (from a in _dbContext.Area
                                  select new DatatableArea
                                  {
                                      id = a.Id,
                                      nombre = a.Nombre,
                                      descripcion = a.Descripcion
                                  }).ToListAsync();
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
        public async Task<bool> ValidarNombre(string nombre)
        {
            var persona = await _dbContext.Area.Where(a => a.Nombre == nombre).FirstOrDefaultAsync();

            if (persona == null)
            {
                return true;
            }
            return false;
        }
        public async Task<Area> Store(AreaCreateDto areaCreateDto)
        {
            var area = _mapper.Map<Area>(areaCreateDto);
            await _dbContext.AddAsync(area);
            await _dbContext.SaveChangesAsync();
            return area;
        }
        public async Task<Area> GetOne(int id)
        {
            return await _dbContext.Area.FindAsync(id);
        }
        public async Task<List<Area>> StoreAll(List<AreaCreateDto> areaCreateDtos)
        {
            var area = _mapper.Map<List<Area>>(areaCreateDtos);
            await _dbContext.AddRangeAsync(area);
            await _dbContext.SaveChangesAsync();
            return area;
        }
        public async Task<List<Area>> GetAllByID(List<int> areas_id)
        {
            var areas = await this._dbContext.Area.Where(area => areas_id.Contains(area.Id)).ToListAsync();
            return areas;
        }
        //revisar esta funcion
        public async Task<Area> Update(Area area)
        {
            _dbContext.Update(area);
            await _dbContext.SaveChangesAsync();
            return area;
        }
    }
}