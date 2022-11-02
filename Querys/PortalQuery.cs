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
        public async Task<List<AreaPortalHelper>> GetAllSelecionadas(int area_id)
        {
            var portals = await this._dBContext.Portal.Where(portal => portal.ControlIdAreaFromId == area_id || portal.ControlIdAreaToId == area_id).ToListAsync();
            var resultado = new List<AreaPortalHelper>();
            foreach (var portal in portals)
            {
                resultado.Add(new AreaPortalHelper
                {
                    ControlId = portal.ControlId,
                    ControlIdAreaFromId = portal.ControlIdAreaFromId,
                    ControlIdAreaToId = portal.ControlIdAreaToId,
                    ControlIdName = portal.ControlIdName,
                    Descripcion = portal.Descripcion,
                    Id = portal.Id,
                    Nombre = portal.Nombre,
                    AreaFromId = portal.AreaFromId,
                    AreaToId = portal.AreaToId,
                    AreaTo = await this._dBContext.Area.Where(a => a.Id == portal.Id).FirstOrDefaultAsync(),
                    AreaFrom = await this._dBContext.Area.Where(a => a.Id == portal.Id).FirstOrDefaultAsync()
                });
            }
            return resultado;
        }
        public async Task<List<AreaPortalHelper>> GetAllDisponibles(int area_id)
        {
            var portals = await this._dBContext.Portal.Where(portal => portal.ControlIdAreaFromId != area_id && portal.ControlIdAreaToId != area_id).ToListAsync();
            var resultado = new List<AreaPortalHelper>();
            foreach (var portal in portals)
            {
                resultado.Add(new AreaPortalHelper
                {
                    ControlId = portal.ControlId,
                    ControlIdAreaFromId = portal.ControlIdAreaFromId,
                    ControlIdAreaToId = portal.ControlIdAreaToId,
                    ControlIdName = portal.ControlIdName,
                    Descripcion = portal.Descripcion,
                    Id = portal.Id,
                    Nombre = portal.Nombre,
                    AreaFromId = portal.AreaFromId,
                    AreaToId = portal.AreaToId,
                    AreaTo = await this._dBContext.Area.Where(a => a.Id == portal.Id).FirstOrDefaultAsync(),
                    AreaFrom = await this._dBContext.Area.Where(a => a.Id == portal.Id).FirstOrDefaultAsync()
                });
            }
            return resultado;
        }
        public async Task<List<AreaPortalHelper>> GetAllDetail()
        {
            var portals = await this._dBContext.Portal.ToListAsync();
            var resultado = new List<AreaPortalHelper>();
            foreach (var portal in portals)
            {
                resultado.Add(new AreaPortalHelper
                {
                    ControlId = portal.ControlId,
                    ControlIdAreaFromId = portal.ControlIdAreaFromId,
                    ControlIdAreaToId = portal.ControlIdAreaToId,
                    ControlIdName = portal.ControlIdName,
                    Descripcion = portal.Descripcion,
                    Id = portal.Id,
                    Nombre = portal.Nombre,
                    AreaFromId = portal.AreaFromId,
                    AreaToId = portal.AreaToId,
                    AreaTo = await this._dBContext.Area.Where(a => a.Id == portal.Id).FirstOrDefaultAsync(),
                    AreaFrom = await this._dBContext.Area.Where(a => a.Id == portal.Id).FirstOrDefaultAsync()
                });
            }
            return resultado;
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
        public async Task<List<Portal>> GetAreaId(int ControlId)
        {
            var portals = await this._dBContext.Portal.Where(p => p.ControlId == ControlId).ToListAsync();
            return portals;
        }
        public async Task<List<Portal>> GetAllAreaId(int area_id)
        {
            var portals = await this._dBContext.Portal
            .Where(portal => portal.ControlIdAreaFromId == area_id || portal.ControlIdAreaToId == area_id)
            .ToListAsync();
            return portals;
        }

        public async Task<Portal> UpdateControlId(Portal portal)
        {
            _dBContext.Entry(await _dBContext.Portal.FirstOrDefaultAsync(p => p.Id == portal.Id)).CurrentValues.SetValues(new
            {
                ControlIdAreaFromId = portal.ControlIdAreaFromId,
                ControlIdAreaToId = portal.ControlIdAreaToId
            });
            await _dBContext.SaveChangesAsync();
            return await _dBContext.Portal.Where(a => a.Id == portal.Id).FirstAsync();
        }
        public async Task<Portal> SearchControlId(int ControlId)
        {
            return await _dBContext.Portal.Where(p => p.ControlId == ControlId).FirstOrDefaultAsync();
        }
    }
    public class AreaPortalHelper
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int ControlId { get; set; }
        public string ControlIdName { get; set; }
        public int ControlIdAreaFromId { get; set; }
        public int AreaFromId { get; set; }
        public Area AreaFrom { get; set; }
        public int ControlIdAreaToId { get; set; }
        public int AreaToId { get; set; }
        public Area AreaTo { get; set; }
    }
}