using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.HorarioReglaAcceso;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

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
        public async Task<List<HorarioReglaAcceso>> StoreAll(List<HorarioReglaAcceso> horarioReglaAccesos)
        {
            await _dBContext.AddRangeAsync(horarioReglaAccesos);
            await _dBContext.SaveChangesAsync();
            var ids = new List<int>();
            foreach (var horarioReglaAcceso in horarioReglaAccesos)
            {
                ids.Add(horarioReglaAcceso.Id);
            }
            return await _dBContext.HorarioReglaAcceso.Where(hr => ids.Contains(hr.Id)).Include(x => x.Horario).ToListAsync();
        }
        public async Task<HorarioReglaAcceso> Store(HorarioReglaAcceso horarioReglaAcceso)
        {
            var resultado = _dBContext.HorarioReglaAcceso.Add(horarioReglaAcceso);
            await _dBContext.SaveChangesAsync();
            return horarioReglaAcceso;
        }
        public async Task<HorarioReglaAcceso> UpdateControlId(HorarioReglaAcceso horarioReglaAcceso)
        {
            _dBContext.Entry(await _dBContext.HorarioReglaAcceso.FirstOrDefaultAsync(x => x.HorarioId == horarioReglaAcceso.HorarioId && x.ReglasAccesoId == horarioReglaAcceso.ReglasAccesoId)).CurrentValues.SetValues(
               new
               {
                   ControlIdAccessRulesId = horarioReglaAcceso.ControlIdAccessRulesId,
                   ControlIdTimeZoneId = horarioReglaAcceso.ControlIdTimeZoneId
               });
            await _dBContext.SaveChangesAsync();
            return await _dBContext.HorarioReglaAcceso.Where(x => x.HorarioId == horarioReglaAcceso.HorarioId && x.ReglasAccesoId == horarioReglaAcceso.ReglasAccesoId).FirstAsync();
        }
        public async Task<bool> DeleteAllReglaAccesoId(int ReglasAccesoId)
        {
            var buscar_horarios = await this._dBContext.HorarioReglaAcceso.Where(x => x.ReglasAccesoId == ReglasAccesoId).ToListAsync();
            _dBContext.HorarioReglaAcceso.RemoveRange(buscar_horarios);
            var resultado = await _dBContext.SaveChangesAsync();
            if (resultado > 0)
            {
                return true;
            }
            return false;
        }
    }
}