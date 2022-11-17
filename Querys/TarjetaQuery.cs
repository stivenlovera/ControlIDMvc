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
        public async Task<bool> VerityCard(int area, int codigo)
        {
            var existCard = await _dbContext.Tarjeta.Where(tarjeta => tarjeta.area == area && tarjeta.codigo == codigo).AnyAsync();
            return existCard;
        }
        public async Task<bool> VerityCardEditar(int area, int codigo, int personaId)
        {
            var existCard = await _dbContext.Tarjeta.Where(tarjeta => tarjeta.area == area && tarjeta.codigo == codigo && tarjeta.PersonaId != personaId).AnyAsync();
            return existCard;
        }
        public async Task<Tarjeta> UpdateOne(Tarjeta tarjeta)
        {
            _dbContext.Update(tarjeta);
            await _dbContext.SaveChangesAsync();
            return tarjeta;
        }
        public async Task<Tarjeta> UpdateOneControlId(Tarjeta tarjeta)
        {
            _dbContext.Entry(await _dbContext.Tarjeta.FirstOrDefaultAsync(x => x.Id == tarjeta.Id)).CurrentValues.SetValues(new
            {
                ControlId = tarjeta.ControlId,
                ControlIdsecret = tarjeta.ControlIdsecret,
                ControlIdValue = tarjeta.ControlIdValue,
                ControlIdUserId = tarjeta.ControlIdUserId
            });
            await _dbContext.SaveChangesAsync();
            return tarjeta;
        }
        public async Task<Tarjeta> GetOne(Tarjeta tarjeta)
        {
            _dbContext.Update(tarjeta);
            await _dbContext.SaveChangesAsync();
            return tarjeta;
        }
        public async Task<List<Tarjeta>> GetAllByPersona(int personaId)
        {
            var listaTarjetas = await _dbContext.Tarjeta.Where(tarjeta => tarjeta.PersonaId == personaId).ToListAsync();
            return listaTarjetas;
        }
    }
}