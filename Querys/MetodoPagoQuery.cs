using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class MetodoPagoQuery
    {
        private readonly DBContext _dbContext;
        public MetodoPagoQuery(
            DBContext dbContext
            )
        {
            this._dbContext = dbContext;
        }
        public async Task<MetodoPago> Store(MetodoPago MetodoPago)
        {
            _dbContext.MetodoPago.Add(MetodoPago);
            await _dbContext.SaveChangesAsync();
            return MetodoPago;
        }
        public async Task<List<MetodoPago>> GetAll()
        {
            return await _dbContext.MetodoPago.ToListAsync();
        }
        public async Task<MetodoPago> ObtenerMetodoPago(int Id)
        {
            return await _dbContext.MetodoPago.Where(x => x.Id == Id).FirstAsync();
        }
        public async Task<List<Inscripcion>> GetAllPersonaId(int PersonaId)
        {
            return await _dbContext.Inscripcion.Where(x => x.PersonaId == PersonaId).ToListAsync();
        }
        public async Task<List<MetodoPago>> GetAllMetodoPagoId(int MetodoPago)
        {
            return await _dbContext.MetodoPago.Where(x => x.Id == MetodoPago).Include(i => i.inscripcion).ToListAsync();
        }
    }
}