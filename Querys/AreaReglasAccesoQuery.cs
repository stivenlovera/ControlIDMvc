using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.AreaReglaAccesoCreateDto;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

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
        public async Task<bool> storeAll(List<AreaReglaAcceso> areaReglaAccesos)
        {
            await _dBContext.AddRangeAsync(areaReglaAccesos);
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
        public async Task<bool> DeleteAllReglaAccesoId(int ReglaAccesoId)
        {
            var buscar_areas = await this._dBContext.AreaReglaAcceso.Where(x => x.ReglaAccesoId == ReglaAccesoId).ToListAsync();
            _dBContext.AreaReglaAcceso.RemoveRange(buscar_areas);
            var resultado = await _dBContext.SaveChangesAsync();
            if (resultado > 0)
            {
                return true;
            }
            return false;
        }
    }
}