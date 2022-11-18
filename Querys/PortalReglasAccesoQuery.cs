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

        public async Task<List<PortalReglaAcceso>> StoreAll(List<PortalReglaAcceso> portalReglaAccesos)
        {
            await _dBContext.AddRangeAsync(portalReglaAccesos);
            await _dBContext.SaveChangesAsync();
            var ids = new List<int>();
            foreach (var portalReglaAcceso in portalReglaAccesos)
            {
                ids.Add(portalReglaAcceso.Id);
            }
            return await _dBContext.PortalReglaAcceso.Where(pr => ids.Contains(pr.Id)).Include(x => x.Portal).ToListAsync();
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
        public async Task<PortalReglaAcceso> UpdateControlId(PortalReglaAcceso portalReglaAcceso)
        {
            _dBContext.Entry(await _dBContext.PortalReglaAcceso.FirstOrDefaultAsync(x => x.PortalId == portalReglaAcceso.PortalId && x.ReglaAccesoId == portalReglaAcceso.ReglaAccesoId)).CurrentValues.SetValues(
               new
               {
                   ControlIdRulesId = portalReglaAcceso.ControlIdRulesId,
                   ControlIdPortalId = portalReglaAcceso.ControlIdPortalId
               });
            await _dBContext.SaveChangesAsync();
            return await _dBContext.PortalReglaAcceso.Where(x => x.PortalId == portalReglaAcceso.PortalId && x.ReglaAccesoId == portalReglaAcceso.ReglaAccesoId).FirstAsync();
        }
    }
}