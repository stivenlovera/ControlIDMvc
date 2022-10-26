using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.Portal;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class PortalQuery
    {
        private readonly IMapper _mapper;
        private readonly DBContext _dBContext;

        public PortalQuery(IMapper mapper, DBContext dBContext)
        {
            this._mapper = mapper;
            this._dBContext = dBContext;
        }
        public async Task<List<Portal>> GetAll()
        {
            return await this._dBContext.Portal.ToListAsync();
        }
        public async Task<bool> StoreAll(List<Portal> portals)
        {
            await _dBContext.AddRangeAsync(portals);
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
        public async Task<List<Portal>> GetAllByID(List<int> portals_id)
        {
            var portals = await this._dBContext.Portal.Where(area => portals_id.Contains(area.Id)).ToListAsync();
            return portals;
        }
        public async Task<List<Portal>> GetAllArea(int area_id)
        {
            var portals = await this._dBContext.Portal
            .Where(portal => portal.ControlIdAreaFromId == area_id)
            .Where(portal => portal.ControlIdAreaToId == area_id)
            .ToListAsync();
            return portals;
        }
        public async Task<bool> UpdateArea(Portal portal, int portal_id)
        {
            if (!await _dBContext.Portal.AnyAsync(portal => portal.Id == portal_id))
            {
                return false;
            }
            _dBContext.Update(portal);
            var resultado = await _dBContext.SaveChangesAsync();
            if (resultado == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Portal> SearchControlId(int ControlId)
        {
            return await _dBContext.Portal.Where(p => p.ControlId == ControlId).FirstOrDefaultAsync();
        }
    }

}