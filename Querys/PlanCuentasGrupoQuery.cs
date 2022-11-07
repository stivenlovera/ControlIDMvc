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
        public async Task<List<PlanCuentaGrupo>> GetAll()
        {
            return await _dBContext.PlanCuentaGrupo.ToListAsync();
        }
        public async Task<PlanCuentaGrupo> GetOne(int planCuentaGrupoId)
        {
            return await _dBContext.PlanCuentaGrupo.Where(p => p.Id == planCuentaGrupoId).FirstOrDefaultAsync();
        }
    }
}