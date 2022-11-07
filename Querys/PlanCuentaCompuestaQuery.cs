using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;

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