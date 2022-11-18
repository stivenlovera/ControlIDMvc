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
        public async Task<List<PlanCuentaSubCuenta>> GetOneCompuestaId(int compuestaId)
        {
            return await _dBContext.PlanCuentaSubCuenta.Where(p => p.PlanCuentaCompuestaId == compuestaId).ToListAsync();
        }
        public bool Update()
        {
            return true;
        }
        public bool delete()
        {
            return true;
        }
        public bool GetAll()
        {
            return true;
        }
    }
}