using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.AreaReglaAccesoCreateDto;
using ControlIDMvc.Entities;

namespace ControlIDMvc.Querys
{
    public class AreaReglasAccesoQuery
    {
        private readonly IMapper _mapper;
        private readonly DBContext _dBContext;

        public AreaReglasAccesoQuery(IMapper mapper, DBContext dBContext)
        {
            this._mapper = mapper;
            this._dBContext = dBContext;
        }
        public async Task<AreaReglaAcceso> store(AreaReglaAccesoCreateDto areaReglaAccesoCreateDto)
        {
            var areaReglaAccesoDto = _mapper.Map<AreaReglaAcceso>(areaReglaAccesoCreateDto);
            await _dBContext.AddAsync(areaReglaAccesoDto);
            var resultado = await _dBContext.SaveChangesAsync();
            return areaReglaAccesoDto;
        }
    }
}