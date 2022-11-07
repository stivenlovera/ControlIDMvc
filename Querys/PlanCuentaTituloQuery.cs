using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;

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