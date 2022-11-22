using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class PlanCuentaCompuestaQuery
    {
        private readonly DBContext _dBContext;

        public PlanCuentaCompuestaQuery(DBContext dBContext)
        {
            this._dBContext = dBContext;
        }
        public async Task<PlanCuentaCompuesta> Store(PlanCuentaCompuesta planCuentaCompuesta)
        {
            await this._dBContext.PlanCuentaCompuesta.AddAsync(planCuentaCompuesta);
            await _dBContext.SaveChangesAsync();
            return planCuentaCompuesta;
        }
        public async Task<PlanCuentaCompuesta> GetOneCompuestaId(int CompuestaId)
        {
            return await _dBContext.PlanCuentaCompuesta.Where(p => p.Id == CompuestaId).Include(p => p.PlanCuentaTitulo).ThenInclude(x => x.PlanCuentaRubro).ThenInclude(x => x.PlanCuentaGrupo).FirstAsync();
        }
        public async Task<bool> ValidarCodigo(string codigo, int codigoTituloId)
        {
            return await _dBContext.PlanCuentaCompuesta.Where(pc => pc.Codigo == codigo && pc.PlanCuentaTituloId == codigoTituloId).AnyAsync();
        }
        public async Task<bool> ValidarCodigoUpdate(int id, string codigo, int cuentaTituloId)
        {
            return await _dBContext.PlanCuentaCompuesta.Where(pg => pg.Codigo == codigo && pg.Id != id && pg.PlanCuentaTituloId == cuentaTituloId).AnyAsync();
        }
        public async Task<PlanCuentaCompuesta> Update(PlanCuentaCompuesta planCuentaCompuesta)
        {
            _dBContext.Entry(await _dBContext.PlanCuentaCompuesta.FirstOrDefaultAsync(x => x.Id == planCuentaCompuesta.Id)).CurrentValues.SetValues(new
            {
                Id = planCuentaCompuesta.Id,
                Codigo = planCuentaCompuesta.Codigo,
                NombreCuenta = planCuentaCompuesta.NombreCuenta
            });
            await _dBContext.SaveChangesAsync();
            return await _dBContext.PlanCuentaCompuesta.Where(pg => pg.Id == planCuentaCompuesta.Id).FirstAsync();
        }
        public async Task<bool> Delete(int id)
        {
            var planCuentaCompuesta = await _dBContext.PlanCuentaCompuesta.Where(x => x.Id == id).FirstAsync();
            if (planCuentaCompuesta != null)
            {
                _dBContext.PlanCuentaCompuesta.Remove(planCuentaCompuesta);
                await _dBContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public bool GetAll()
        {
            return true;
        }
    }
}