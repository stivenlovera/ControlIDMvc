using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.Egreso;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.DatatableModel;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class EgresoQuery
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;

        public EgresoQuery(DBContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }
        public object DataTable(HttpRequest httpRequest)
        {
            int totalRecord = 0;
            int filterRecord = 0;

            var draw = httpRequest.Form["draw"].FirstOrDefault();
            var sortColumn = httpRequest.Form["columns[" + httpRequest.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = httpRequest.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = httpRequest.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(httpRequest.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(httpRequest.Form["start"].FirstOrDefault() ?? "0");

            var data = (from e in _dbContext.Egreso
                        select new DataTableEgreso()
                        {
                            Id = e.Id,
                            Concepto = e.Concepto,
                            FechaCreacion = e.FechaCreacion.ToString("dddd, dd MMMM yyyy"),
                            Monto = e.Monto,
                            NumeroRecibo = e.FechaCreacion.ToString("yyyyMMddHHmmss"),
                            Usuario = e.Usuario.User
                        });

            totalRecord = data.Count();
            // buscar por valor
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x => x.Concepto.ToLower().Contains(searchValue.ToLower()) || x.Monto.ToString().ToLower().Contains(searchValue.ToLower()) || x.Usuario.ToLower().Contains(searchValue.ToLower()) || x.NumeroRecibo.ToString().ToLower().Contains(searchValue.ToLower()));
            }
            // get total count of records after search
            filterRecord = data.Count();
            System.Console.WriteLine(" filtro " + sortColumn + " " + sortColumnDirection);
            //filtro columna
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection)) data = data.OrderBy(x => sortColumn).ThenBy(x => sortColumnDirection);
            //pagination
            var empList = data.Skip(skip).Take(pageSize).ToList();
            return new
            {
                draw = draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = empList
            };


        }
        public async Task<Egreso> Store(EgresoCreateDto createEgresoDto)
        {
            var egreso = _mapper.Map<Egreso>(createEgresoDto);
            _dbContext.Egreso.Add(egreso);
            await _dbContext.SaveChangesAsync();
            return egreso;
        }

        public async Task<Egreso> Edit(int id)
        {
            return await _dbContext.Egreso.FindAsync(id);
        }

        public async Task<Egreso> Update(Egreso egreso)
        {
            _dbContext.Update(egreso);
            await _dbContext.SaveChangesAsync();
            return egreso;
        }
        public async Task<bool> Delete(int id)
        {
            var existe = await _dbContext.Inscripcion.AnyAsync(x => x.Id == id);
            if (existe)
            {
                _dbContext.Remove(new Egreso() { Id = id });
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}