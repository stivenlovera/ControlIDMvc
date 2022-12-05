using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class MovimientoAsientoQuery
    {
        private readonly DBContext _dbContext;

        public MovimientoAsientoQuery(DBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<MovimientosAsiento> Store(MovimientosAsiento movimientosAsiento)
        {
            _dbContext.MovimientosAsiento.Add(movimientosAsiento);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.MovimientosAsiento.Where(x => x.Id == movimientosAsiento.Id).Include(x => x.Asientos).FirstAsync();
        }
        public async Task<MovimientosAsiento> GetOne(int id)
        {
            return await _dbContext.MovimientosAsiento.Where(x => x.Id == id).Include(x => x.Asientos).ThenInclude(x => x.PlanAsientos).FirstAsync();
        }
        public async Task<List<MovimientosAsiento>> LikePersonaId(int PersonaId)
        {
            return await _dbContext.MovimientosAsiento.Where(x => x.PersonaId.ToString().Contains(PersonaId.ToString())).Include(x=>x.Asientos).ToListAsync();
        }
        public async Task<List<MovimientosAsiento>> ShowAllGrl()
        {
            return await _dbContext.MovimientosAsiento.Include(X => X.Persona).Include(x => x.TipoMovimiento).ToListAsync();
        }
        public async Task<MovimientosAsiento> Update(MovimientosAsiento movimientosAsiento)
        {
            _dbContext.Entry(await _dbContext.MovimientosAsiento.FirstOrDefaultAsync(x => x.Id == movimientosAsiento.Id)).CurrentValues.SetValues(new
            {
                Id = movimientosAsiento.Id,
                Monto = movimientosAsiento.Monto,
                EntregeA = movimientosAsiento.EntregeA,
                MontoLiteral = movimientosAsiento.MontoLiteral,
            });
            await _dbContext.SaveChangesAsync();
            return await _dbContext.MovimientosAsiento.Where(p => p.Id == movimientosAsiento.Id).Include(x => x.Asientos).FirstAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var movimiento = await _dbContext.MovimientosAsiento.Where(x => x.Id == id).FirstAsync();
            if (movimiento != null)
            {
                _dbContext.MovimientosAsiento.Remove(movimiento);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        
    }
}