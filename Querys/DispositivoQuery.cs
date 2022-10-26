using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControlIDMvc.Dtos.Dispositivo;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.DatatableModel;
using ControlIDMvc.ServicesCI;
using ControlIDMvc.ServicesCI.QueryCI;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class DispositivoQuery
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;
        public DispositivoQuery(
            HttpClientService httpClientService,
            DBContext dbContext,
            IMapper mapper
            )
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        /* Datatable */
        public string draw;
        public string start;
        public string length;
        public string showColumn;
        public string showColumnDir;
        public string searchValue;
        public int pageSize, skip, recordsTotal;

        public AccessRulesControlIdQuery AccessRulesControlIdQuery { get; }

        public async Task<List<Dispositivo>> getLoginControlador(int controlador_id, int proyecto_id)
        {
            var dispostivos = await this._dbContext.Dispositivo
            .Where(dispositivo => dispositivo.Id == controlador_id)
            .ToListAsync();
            return dispostivos;
        }
        public async Task<object> DataTable(HttpRequest httpRequest)
        {
            var draw = httpRequest.Form["draw"].FirstOrDefault();
            var start = httpRequest.Form["start"].FirstOrDefault();
            var length = httpRequest.Form["length"].FirstOrDefault();
            var sortColumna = httpRequest.Form["column[" + httpRequest.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnaDir = httpRequest.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = httpRequest.Form["search[value]"].FirstOrDefault();

            pageSize = length != null ? Convert.ToInt32(length) : 0;
            skip = start != null ? Convert.ToInt32(start) : 0;
            recordsTotal = 0;

            List<DatatableDispositivo> dispositivo = new List<DatatableDispositivo>();
            using (_dbContext)
            {
                dispositivo = await (from a in _dbContext.Dispositivo
                                     select new DatatableDispositivo
                                     {
                                         id = a.Id,
                                         nombre = a.Nombre,
                                         controlIdIp = a.ControlIdIp,
                                         controlIdName = a.ControlIdName
                                     }).ToListAsync();
                recordsTotal = dispositivo.Count();
                dispositivo = dispositivo.Skip(skip).Take(pageSize).ToList();
                System.Console.WriteLine($"el total de registros es : {dispositivo.Count()}");
            }
            return new
            {
                draw = "draw",
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = dispositivo
            };
        }
        public async Task<Dispositivo> Store(DispositivoCreateDto dispositivoCreateDto)
        {
            var dispositivo = _mapper.Map<Dispositivo>(dispositivoCreateDto);
            await _dbContext.AddAsync(dispositivo);
            var resultado = await _dbContext.SaveChangesAsync();
            return dispositivo;
        }
        public async Task<List<Dispositivo>> GetAll()
        {
            return await _dbContext.Dispositivo.ToListAsync();
        }
        public async Task<List<Dispositivo>> GetOne(int id)
        {
            var dispostivos = await this._dbContext.Dispositivo.Where(dispositivo => dispositivo.Id == id).ToListAsync();
            return dispostivos;
        }

        public async Task<List<Dispositivo>> GetAllByID(List<int> dispositivos_id)
        {
            var dispostivos = await this._dbContext.Dispositivo.Where(dispositivo => dispositivos_id.Contains(dispositivo.Id)).ToListAsync();
            return dispostivos;
        }
        public async Task<List<Dispositivo>> GetAllPuertas(int dispositivos_id)
        {
            var dispostivos = await this._dbContext.Dispositivo.Where(dispositivo => dispositivo.Id == dispositivos_id).Include(d => d.Portals).ToListAsync();
            return dispostivos;
        }
 
    }
}