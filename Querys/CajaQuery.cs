using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.Caja;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.DatatableModel;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class CajaQuery
    {
        private readonly IMapper _mapper;
        private readonly DBContext _dbContext;

        public CajaQuery(DBContext dbContext, IMapper mapper)
        {
            this._mapper = mapper;
            this._dbContext = dbContext;
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

            var data = (from c in _dbContext.Caja
                        select new DataTableCaja()
                        {
                            Id = c.Id,
                            Concepto = c.Concepto,
                            Fecha = c.Fecha.ToString("dd/MM/yyyy HH:mm"),
                            Ingreso = c.Tipo != "ingreso" ? 0 : c.Valor,
                            Egreso = c.Tipo != "egreso" ? 0 : c.Valor,
                            NumeroRecibo = c.NumeroRecibo,
                            Usuario = "buscar origen",
                            Entregado = c.Persona,
                            Tipo = c.Tipo
                        });
            /*FILTROS POR URL*/
            System.Console.WriteLine("VALOR A EVALUAR: " + httpRequest.Query["tipo"].ToString());
            /* var param=httpRequest.Query["tipo"].ToString();
            if (httpRequest.Query["tipo"].ToString() != "")
            {
                data.Where(c => c.Tipo == httpRequest.Query["tipo"].ToString());
            } */

            totalRecord = data.Count();
            // buscar por valor
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x => x.Concepto.ToLower().Contains(searchValue.ToLower()) || x.Egreso.ToString().ToLower().Contains(searchValue.ToLower()) || x.Ingreso.ToString().ToLower().Contains(searchValue.ToLower()) || x.Usuario.ToLower().Contains(searchValue.ToLower()) || x.NumeroRecibo.ToString().ToLower().Contains(searchValue.ToLower()) || x.Tipo.ToString().ToLower().Contains(searchValue.ToLower()));
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
        public async Task<Caja> Store(CajaCreateDto createCajaDto)
        {
            var caja = _mapper.Map<Caja>(createCajaDto);
            _dbContext.Caja.Add(caja);
            await _dbContext.SaveChangesAsync();
            return caja;
        }

        public async Task<Caja> Edit(int id)
        {
            return await _dbContext.Caja.FindAsync(id);
        }

        public async Task<Caja> Update(Caja caja)
        {
            _dbContext.Update(caja);
            await _dbContext.SaveChangesAsync();
            return caja;
        }
        public async Task<bool> Delete(int id)
        {
            var existe = await _dbContext.Inscripcion.AnyAsync(x => x.Id == id);
            if (existe)
            {
                _dbContext.Remove(new Caja() { Id = id });
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}