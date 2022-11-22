using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class PlanCuentaTituloQuery
    {
        private readonly DBContext _dBContext;

        public PlanCuentaTituloQuery(DBContext dBContext)
        {
            this._dBContext = dBContext;
        }
        public async Task<PlanCuentaTitulo> Store(PlanCuentaTitulo planCuentaTitulo)
        {
            await this._dBContext.PlanCuentaTitulo.AddAsync(planCuentaTitulo);
            await _dBContext.SaveChangesAsync();
            return planCuentaTitulo;
        }
        public async Task<List<PlanCuentaTitulo>> GetOneRubroId(int rubroId)
        {
            return await _dBContext.PlanCuentaTitulo.Where(p => p.PlanCuentaRubroId == rubroId).Include(p => p.PlanCuentaCompuesta).ToListAsync();
        }
        public async Task<PlanCuentaTitulo> GetOnetituloId(int TituloId)
        {
            return await _dBContext.PlanCuentaTitulo.Where(p => p.Id == TituloId).Include(p => p.PlanCuentaRubro).ThenInclude(x => x.PlanCuentaGrupo).FirstAsync();
        }
        public async Task<bool> ValidarCodigo(string codigo, int codigoRubroId)
        {
            return await _dBContext.PlanCuentaTitulo.Where(pt => pt.Codigo == codigo && pt.PlanCuentaRubroId == codigoRubroId).AnyAsync();
        }
        public async Task<bool> ValidarCodigoUpdate(int id, string codigo, int codigoRubroId)
        {
            return await _dBContext.PlanCuentaTitulo.Where(pg => pg.Codigo == codigo && pg.Id != id &&  pg.PlanCuentaRubroId ==codigoRubroId).AnyAsync();
        }
        public async Task<PlanCuentaTitulo> Update(PlanCuentaTitulo planCuentaTitulo)
        {
            _dBContext.Entry(await _dBContext.PlanCuentaTitulo.FirstOrDefaultAsync(x => x.Id == planCuentaTitulo.Id)).CurrentValues.SetValues(new
            {
                Id = planCuentaTitulo.Id,
                Codigo = planCuentaTitulo.Codigo,
                NombreCuenta = planCuentaTitulo.NombreCuenta
            });
            await _dBContext.SaveChangesAsync();
            return await _dBContext.PlanCuentaTitulo.Where(pg => pg.Id == planCuentaTitulo.Id).FirstAsync();
        }
        public async Task<bool> Delete(int id)
        {
            var planCuentaTitulo = await _dBContext.PlanCuentaTitulo.Where(x => x.Id == id).FirstAsync();
            if (planCuentaTitulo != null)
            {
                _dBContext.PlanCuentaTitulo.Remove(planCuentaTitulo);
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