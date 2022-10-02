using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.AccionPortal;
using ControlIDMvc.Entities;

namespace ControlIDMvc.Querys
{
    public class PortalAccionQuery
    {
        private readonly IMapper _mapper;
        private readonly DBContext _dBContext;

        public PortalAccionQuery(IMapper mapper, DBContext dBContext)
        {
            this._mapper = mapper;
            this._dBContext = dBContext;
        }
        public async Task<bool> store(AccionPortalCreateDto accionPortalCreateDto)
        {
            var accionPortal = _mapper.Map<AccionPortal>(accionPortalCreateDto);
            await _dBContext.AddAsync(accionPortal);
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