using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.Accion;
using ControlIDMvc.Entities;

namespace ControlIDMvc.Querys
{
    public class AccionQuery
    {
        private readonly IMapper _mapper;
        private readonly DBContext _dBContext;

        public AccionQuery(IMapper mapper, DBContext dBContext)
        {
            this._mapper = mapper;
            this._dBContext = dBContext;
        }
        public async Task<bool> store(AccionCreateDto accionCreateDto)
        {
            var accion = _mapper.Map<Accion>(accionCreateDto);
            await _dBContext.AddAsync(accion);
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