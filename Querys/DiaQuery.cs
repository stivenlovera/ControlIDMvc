using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class DiaQuery
    {
        private readonly DBContext _dBContext;

        public DiaQuery(DBContext dBContext)
        {
            this._dBContext = dBContext;
        }
        public async Task<bool> StoreAll(List<Dia> dias)
        {
            await _dBContext.AddRangeAsync(dias);
            var resultado = await _dBContext.SaveChangesAsync();
            if (resultado == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Dia> Store(Dia dias)
        {
            await _dBContext.Dia.AddAsync(dias);
            var resultado = await _dBContext.SaveChangesAsync();
            return dias;
        }
        public async Task<List<Dia>> ShowAll(Dia dias)
        {
           return await _dBContext.Dia.ToListAsync();
        }
    }
}