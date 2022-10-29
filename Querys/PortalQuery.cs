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
        }public async Task<List<Portal>> GetAreaId(int ControlId)
        {
            var portals = await this._dBContext.Portal.Where(p => p.ControlId==ControlId).ToListAsync();
            return portals;
        }
        public async Task<List<Portal>> GetAllAreaId(int area_id)
        {
            var portals = await this._dBContext.Portal
            .Where(portal => portal.ControlIdAreaFromId == area_id)
            .Where(portal => portal.ControlIdAreaToId == area_id)
            .ToListAsync();
            return portals;
        }
        public async Task<Portal> UpdateControlId(Portal portal)
        {
            _dBContext.Entry(await _dBContext.Portal.FirstOrDefaultAsync(p => p.Id == portal.Id)).CurrentValues.SetValues(new
            {
                ControlIdAreaFromId=portal.ControlIdAreaFromId,
                ControlIdAreaToId=portal.ControlIdAreaToId
            });
            await _dBContext.SaveChangesAsync();
            return await _dBContext.Portal.Where(a => a.Id == portal.Id).FirstAsync();
        }
        public async Task<Portal> SearchControlId(int ControlId)
        {
            return await _dBContext.Portal.Where(p => p.ControlId == ControlId).FirstOrDefaultAsync();
        }
    }

}