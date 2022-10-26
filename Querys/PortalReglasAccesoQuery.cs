using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.PortalReglaAcceso;
using ControlIDMvc.Entities;

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
    }
}