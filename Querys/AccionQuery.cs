using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.Accion;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class AccionQuery
    {
        private readonly IMapper _mapper;
        private readonly DBContext _dBContext;

        public AccionQuery(IMapper mapper, DBContext dBContext)
        {
            this._mapper = mapper;
            this._dBContext = dBContext;
        }
        public async Task<Accion> store(Accion accion)
        {
            await _dBContext.AddAsync(accion);
            await _dBContext.SaveChangesAsync();
            return accion;
        }
        public async Task<bool> AllStore(List<Accion> accions)
        {
            await _dBContext.AddRangeAsync(accions);
            var response = await _dBContext.SaveChangesAsync();
            if (response > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Accion> GetAll(Accion accion)
        {
            await _dBContext.AddAsync(accion);
            await _dBContext.SaveChangesAsync();
            return accion;
        }
        public async Task<Accion> SearchControlId(int ControlId)
        {
            return await _dBContext.Accion.Where(a => a.ControlId == ControlId).FirstOrDefaultAsync();
        }
    }
}