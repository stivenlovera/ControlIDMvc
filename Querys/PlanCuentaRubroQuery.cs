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
        public async Task<List<PlanCuentaRubro>> GetOne(int id)
        {
            return await _dBContext.PlanCuentaRubro.Where(p => p.Id == id).Include(p => p.PlanCuentaTitulo).ToListAsync();
        }
        public bool Update()
        {
            return true;
        }
        public bool delete()
        {
            return true;
        }

    }
}