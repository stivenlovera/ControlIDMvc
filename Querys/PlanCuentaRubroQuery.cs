using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;

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