using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class AsientoQuery
    {
        private readonly DBContext _dbContext;

        public AsientoQuery(DBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Asiento> Store(Asiento asiento)
        {
            _dbContext.Asiento.Add(asiento);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Asiento.Where(x => x.Id == asiento.Id).Include(x => x.MovimientosAsiento).FirstAsync();
        }
        public async Task<List<Asiento>> GetAllDetail(int Id)
        {
            return await _dbContext.Asiento.Where(x => x.MovimientosAsientoId == Id).Include(x => x.PlanAsientos).ToListAsync();
        }
        public async Task<Asiento> Update(Asiento asiento)
        {
            _dbContext.Entry(await _dbContext.Asiento.FirstOrDefaultAsync(x => x.Id == asiento.Id)).CurrentValues.SetValues(new
            {
                Monto = asiento.Monto,
                NombreAsiento = asiento.NombreAsiento
            });
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Asiento.Where(p => p.Id == asiento.Id).Include(x => x.PlanAsientos).FirstAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var asiento = await _dbContext.Asiento.Where(x => x.Id == id).FirstAsync();
            if (asiento != null)
            {
                _dbContext.Asiento.Remove(asiento);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteMovimientoId(int id)
        {
            var asientos = await _dbContext.Asiento.Where(x => x.MovimientosAsientoId == id).ToListAsync();
            if (asientos.Count() > 0)
            {
                _dbContext.Asiento.RemoveRange(asientos);
                await _dbContext.SaveChangesAsync();
                return true;
            }


            return false;
        }
    }
}