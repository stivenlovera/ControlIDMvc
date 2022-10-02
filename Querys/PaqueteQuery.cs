using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.Paquete;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.DatatableModel;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class PaqueteQuery
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;
        public PaqueteQuery(DBContext dbContext, IMapper mapper)
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

            List<DataTablePaquete> paquetes = new List<DataTablePaquete>();
            using (_dbContext)
            {
                paquetes = (from d in _dbContext.Paquete
                            select new DataTablePaquete
                            {
                                Id = d.Id,
                                FechaCreacion = d.FechaCreacion,
                                Dias = d.Dias,
                                Costo = d.Costo,
                                Nombre = d.Nombre
                            }).ToList();
                recordsTotal = paquetes.Count();
                paquetes = paquetes.Skip(skip).Take(pageSize).ToList();
                System.Console.WriteLine($"el total de registros es : {paquetes.Count()}");
            }
            return new
            {
                draw = "draw",
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = paquetes
            };
        }
        public async Task<List<PaqueteDto>> GetAll()
        {
            var paquetes = await this._dbContext.Paquete.ToListAsync();
            var resultado = _mapper.Map<List<PaqueteDto>>(paquetes);
            return resultado;
        }
        public async Task<List<PaqueteDto>> GetAllLike(string value)
        {
            var paquetes = await this._dbContext.Paquete.Where(p => p.Nombre.Contains(value)).ToListAsync();
            var resultado = _mapper.Map<List<PaqueteDto>>(paquetes);
            return resultado;
        }
        public async Task<PaqueteDto> Store(PaqueteCreateDto paqueteCreateDto)
        {
            var paquete = _mapper.Map<Paquete>(paqueteCreateDto);
            await _dbContext.AddAsync(paquete);
            await _dbContext.SaveChangesAsync();
            var resultado = _mapper.Map<PaqueteDto>(paquete);
            return resultado;
        }
        public async Task<PaqueteDto> Update(PaqueteCreateDto paqueteCreateDto)
        {
            var paquete = _mapper.Map<Paquete>(paqueteCreateDto);
            await _dbContext.AddAsync(paquete);
            await _dbContext.SaveChangesAsync();
            var resultado = _mapper.Map<PaqueteDto>(paquete);
            return resultado;
        }
    }

}