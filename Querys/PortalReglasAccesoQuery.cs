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

        public async Task<bool> store(PortalReglaAccesoCreateDto portalReglaAccesoCreateDto)
        {
            var portalReglaAcceso = _mapper.Map<PortalReglaAcceso>(portalReglaAccesoCreateDto);
            await _dBContext.AddAsync(portalReglaAcceso);
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