using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class PlanAsientoQuery
    {
        private readonly DBContext _dbContext;
        public PlanAsientoQuery(DBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<PlanAsiento> Store(PlanAsiento planAsiento)
        {
            _dbContext.PlanAsiento.Add(planAsiento);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.PlanAsiento.Where(x => x.Id == planAsiento.Id).Include(x => x.Asiento).FirstAsync();
        }
        public async Task<List<PlanAsiento>> PlanAsientosByPersonaId(int PersonaId)
        {
            var result = from p in _dbContext.PlanAsiento
                         join a in _dbContext.Asiento on p.AsientoId equals a.Id
                         join m in _dbContext.MovimientosAsiento on a.MovimientosAsientoId equals m.Id
                         where m.PersonaId==PersonaId
                         select p;
            var planesAsientoPersona = await result.ToListAsync();

            return planesAsientoPersona;
        }
    }
}