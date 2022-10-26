using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.AccionPortal;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

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
        public async Task<bool> storeAll(List<AccionPortal> accionPortals)
        {
            await _dBContext.AddRangeAsync(accionPortals);
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