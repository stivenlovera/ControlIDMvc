using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class PlanCuentaSubCuentaQuery
    {
        private readonly DBContext _dBContext;

        public PlanCuentaSubCuentaQuery(DBContext dBContext)
        {
            this._dBContext = dBContext;
        }
        public async Task<PlanCuentaSubCuenta> Store(PlanCuentaSubCuenta planCuentaSubCuenta)
        {
            await this._dBContext.PlanCuentaSubCuenta.AddAsync(planCuentaSubCuenta);
            await _dBContext.SaveChangesAsync();
            return planCuentaSubCuenta;
        }
        public async Task<PlanCuentaSubCuenta> GetOneSubCuentaId(int compuestaId)
        {
            return await _dBContext.PlanCuentaSubCuenta.Where(p => p.PlanCuentaCompuestaId == compuestaId).Include(x => x.PlanCuentaCompuesta).ThenInclude(x => x.PlanCuentaTitulo).ThenInclude(x => x.PlanCuentaRubro).ThenInclude(x => x.PlanCuentaGrupo).FirstAsync();
        }
        public async Task<List<PlanCuentaSubCuenta>> GetSubCuentaId(int compuestaId)
        {
            return await _dBContext.PlanCuentaSubCuenta.Where(p => p.PlanCuentaCompuestaId == compuestaId).ToListAsync();
        }
        public async Task<bool> ValidarCodigo(string codigo, int codigoCompuestaId)
        {
            return await _dBContext.PlanCuentaSubCuenta.Where(psc => psc.Codigo == codigo && psc.PlanCuentaCompuestaId == codigoCompuestaId).AnyAsync();
        }
        public async Task<bool> ValidarCodigoUpdate(int id, string codigo, int cuentaCompuestaId)
        {
            return await _dBContext.PlanCuentaSubCuenta.Where(pg => pg.Codigo == codigo && pg.Id != id && pg.PlanCuentaCompuestaId == cuentaCompuestaId).AnyAsync();
        }
        public async Task<PlanCuentaSubCuenta> Update(PlanCuentaSubCuenta planCuentaSubCuenta)
        {
            _dBContext.Entry(await _dBContext.PlanCuentaSubCuenta.FirstOrDefaultAsync(x => x.Id == planCuentaSubCuenta.Id)).CurrentValues.SetValues(new
            {
                Id = planCuentaSubCuenta.Id,
                Codigo = planCuentaSubCuenta.Codigo,
                NombreCuenta = planCuentaSubCuenta.NombreCuenta,
                Valor = planCuentaSubCuenta.Valor,
                Moneda = planCuentaSubCuenta.Moneda,
            });
            await _dBContext.SaveChangesAsync();
            return await _dBContext.PlanCuentaSubCuenta.Where(pg => pg.Id == planCuentaSubCuenta.Id).FirstAsync();
        }
        public async Task<bool> Delete(int id)
        {
            var planCuentaSubCuenta = await _dBContext.PlanCuentaSubCuenta.Where(x => x.Id == id).FirstAsync();
            if (planCuentaSubCuenta != null)
            {
                _dBContext.PlanCuentaSubCuenta.Remove(planCuentaSubCuenta);
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