using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.HorarioReglaAcceso;
using ControlIDMvc.Entities;

namespace ControlIDMvc.Querys
{
    public class HorarioReglaAccesoQuery
    {
        private readonly IMapper _mapper;
        private readonly DBContext _dBContext;

        public HorarioReglaAccesoQuery(IMapper mapper, DBContext dBContext)
        {
            this._mapper = mapper;
            this._dBContext = dBContext;
        }
        public async Task<bool> StoreAll(List<HorarioReglaAcceso> horarioReglaAccesos)
        {
            await _dBContext.AddRangeAsync(horarioReglaAccesos);
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
        public async Task<HorarioReglaAcceso> Store(HorarioReglaAcceso horarioReglaAcceso)
        {
            var resultado = _dBContext.HorarioReglaAcceso.Add(horarioReglaAcceso);
            await _dBContext.SaveChangesAsync();
            return horarioReglaAcceso;
        }
        
    }
}