using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class PlanCuentaRubroQuery
    {
        private readonly DBContext _dBContext;

        public PlanCuentaRubroQuery(DBContext dBContext)
        {
            this._dBContext = dBContext;
        }
        public async Task<PlanCuentaRubro> Store(PlanCuentaRubro planCuentaRubro)
        {
            await this._dBContext.PlanCuentaRubro.AddAsync(planCuentaRubro);
            await _dBContext.SaveChangesAsync();
            return planCuentaRubro;
        }
        public async Task<List<PlanCuentaRubro>> GetAll()
        {
            return await _dBContext.PlanCuentaRubro.Include(p => p.PlanCuentaTitulo).ToListAsync();
        }
        public async Task<PlanCuentaRubro> GetOne(int id)
        {
            return await _dBContext.PlanCuentaRubro.Where(p => p.Id == id).Include(p => p.PlanCuentaGrupo).FirstAsync();
        }
        /*validaiones*/
        public async Task<bool> ValidarCodigo(string codigo, int codigoGrupoId)
        {
            return await _dBContext.PlanCuentaRubro.Where(pg => pg.Codigo == codigo && pg.PlanCuentaGrupoId==codigoGrupoId).AnyAsync();
        }
        public async Task<bool> ValidarCodigoUpdate(int id, string codigo,int codigoGrupoId)
        {
            return await _dBContext.PlanCuentaRubro.Where(pg => pg.Codigo == codigo && pg.Id != id && pg.PlanCuentaGrupoId == codigoGrupoId).AnyAsync();
        }
        public async Task<PlanCuentaRubro> Update(PlanCuentaRubro planCuentaRubro)
        {
            _dBContext.Entry(await _dBContext.PlanCuentaRubro.FirstOrDefaultAsync(x => x.Id == planCuentaRubro.Id)).CurrentValues.SetValues(new
            {
                Id = planCuentaRubro.Id,
                Codigo = planCuentaRubro.Codigo,
                NombreCuenta = planCuentaRubro.NombreCuenta
            });
            await _dBContext.SaveChangesAsync();
            return await _dBContext.PlanCuentaRubro.Where(pg => pg.Id == planCuentaRubro.Id).FirstAsync();
        }
        public async Task<bool> Delete(int id)
        {
            var planRubro = await _dBContext.PlanCuentaRubro.Where(x => x.Id == id).FirstAsync();
            if (planRubro != null)
            {
                _dBContext.PlanCuentaRubro.Remove(planRubro);
                await _dBContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}