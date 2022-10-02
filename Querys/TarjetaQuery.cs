using AutoMapper;
using ControlIDMvc.Dtos.Tarjeta;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class TarjetaQuery
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;

        public TarjetaQuery(DBContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }
        public async Task<bool> Store(TarjetaCreateDto tarjetaCreateDto)
        {
            var tarjeta = _mapper.Map<Tarjeta>(tarjetaCreateDto);
            _dbContext.Tarjeta.Add(tarjeta);
            var resultado = await _dbContext.SaveChangesAsync();
            if (resultado == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<List<TarjetaDto>> GetAllByPersona(int personaId)
        {
            var listaTarjetas=await _dbContext.Tarjeta.Where(tarjeta=>tarjeta.PersonaId==personaId).ToListAsync();
            var resultado = _mapper.Map<List<TarjetaDto>>(listaTarjetas);
            return resultado;
        }
    }
}