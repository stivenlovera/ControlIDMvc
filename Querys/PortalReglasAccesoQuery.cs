using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.PortalReglaAcceso;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class PortalReglasAccesoQuery
    {
        private readonly IMapper _mapper;
        private readonly DBContext _dBContext;

        public PortalReglasAccesoQuery(IMapper mapper, DBContext dBContext)
        {
            this._mapper = mapper;
            this._dBContext = dBContext;
        }

        public async Task<bool> StoreAll(List<PortalReglaAcceso> portalReglaAcceso)
        {
            await _dBContext.AddRangeAsync(portalReglaAcceso);
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
        public async Task<bool> DeleteAllReglaAccesoId(int ReglaAccesoId)
        {
            var buscar_portals = await this._dBContext.PortalReglaAcceso.Where(x => x.ReglaAccesoId == ReglaAccesoId).ToListAsync();
            _dBContext.PortalReglaAcceso.RemoveRange(buscar_portals);
            var resultado = await _dBContext.SaveChangesAsync();
            if (resultado > 0)
            {
                return true;
            }
            return false;
        }
    }
}