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
        public async Task<Dia> UpdateControlId(Dia dia)
        {
            _dBContext.Entry(await _dBContext.Dia.FirstOrDefaultAsync(x => x.Id == dia.Id)).CurrentValues.SetValues(
               new 
               {
                   Id = dia.Id,
                   ControlId = dia.ControlId,
                   ControlMon = dia.Mon,
                   ControlThu = dia.Thu,
                   ControlWed = dia.Wed,
                   ControlTue = dia.Tue,
                   ControlFri = dia.Fri,
                   ControlSat = dia.Sat,
                   ControlSun = dia.Sun,
                   ControlHol1 = dia.Hol1,
                   ControlHol2 = dia.Hol2,
                   ControlHol3 = dia.Hol3,
                   ControlTimeZoneId = dia.ControlTimeZoneId
               });
            await _dBContext.SaveChangesAsync();
            return await _dBContext.Dia.Where(x => x.Id == dia.Id).FirstAsync();
        }
        public async Task<bool> Delete(int id)
        {
            var dia = await _dBContext.Dia.Where(x => x.Id == id).FirstAsync();
            if (dia != null)
            {
                _dBContext.Dia.Remove(dia);
                await _dBContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Dia>> ShowAll(Dia dias)
        {
            return await _dBContext.Dia.ToListAsync();
        }
    }
}