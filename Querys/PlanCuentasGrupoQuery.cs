using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class PlanCuentasGrupoQuery
    {
        private readonly DBContext _dBContext;

        public PlanCuentasGrupoQuery(DBContext dBContext)
        {
            this._dBContext = dBContext;
        }
        public async Task<PlanCuentaGrupo> Store(PlanCuentaGrupo planCuentaGrupo)
        {
            await _dBContext.AddAsync(planCuentaGrupo);
            await _dBContext.SaveChangesAsync();
            return planCuentaGrupo;
        }
        public async Task<PlanCuentaGrupo> Update(PlanCuentaGrupo planCuentaGrupo)
        {
            _dBContext.Entry(await _dBContext.PlanCuentaGrupo.FirstOrDefaultAsync(x => x.Id == planCuentaGrupo.Id)).CurrentValues.SetValues(new
            {
                Id = planCuentaGrupo.Id,
                Codigo = planCuentaGrupo.Codigo,
                NombreCuenta = planCuentaGrupo.NombreCuenta
            });
            await _dBContext.SaveChangesAsync();
            return await _dBContext.PlanCuentaGrupo.Where(pg => pg.Id == planCuentaGrupo.Id).FirstAsync();
        }
        public async Task<bool> Delete(int id)
        {
            var planGrupo = await _dBContext.PlanCuentaGrupo.Where(x => x.Id == id).FirstAsync();
            if (planGrupo != null)
            {
                _dBContext.PlanCuentaGrupo.Remove(planGrupo);
                await _dBContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<PlanCuentaGrupo>> GetAll()
        {
            return await _dBContext.PlanCuentaGrupo.Include(p => p.PlanCuentaRubros).ToListAsync();
        }
        public async Task<PlanCuentaGrupo> GetOne(int id)
        {
            return await _dBContext.PlanCuentaGrupo.Where(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<bool> ValidarCodigo(string codigo)
        {
            return await _dBContext.PlanCuentaGrupo.Where(pg => pg.Codigo == codigo).AnyAsync();
        }
        public async Task<bool> ValidarCodigoUpdate(int id, string codigo)
        {
            return await _dBContext.PlanCuentaGrupo.Where(pg => pg.Codigo == codigo && pg.Id != id).AnyAsync();
        }
    }
}