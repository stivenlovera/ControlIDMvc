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
        public async Task<List<PortalDto>> GetAll()
        {

            var list_portals = await this._dBContext.Portal.ToListAsync();
            var portals = _mapper.Map<List<PortalDto>>(list_portals);
            return portals;
        }
        public async Task<bool> store(PortalCreateDto portalCreateDto)
        {
            var portal = _mapper.Map<Portal>(portalCreateDto);
            await _dBContext.AddAsync(portal);
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
    }

}