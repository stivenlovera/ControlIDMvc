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