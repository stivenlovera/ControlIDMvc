using AutoMapper;
using ControlIDMvc.Dtos.Horario;
using ControlIDMvc.Entities;
using ControlIDMvc.Models.DatatableModel;
using ControlIDMvc.Models.ModelForm;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class HorarioQuery
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;
        public HorarioQuery(DBContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        /* propiedades */
        public string draw;
        public string start;
        public string length;
        public string showColumn;
        public string showColumnDir;
        public string searchValue;
        public int pageSize, skip, recordsTotal;
        public async Task<object> Datatable(HttpRequest httpRequest)
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

            List<DatatableHorario> horarios = new List<DatatableHorario>();
            var horarios_aux = await _dbContext.Horario.Include(h => h.Dias).ToListAsync();
            foreach (var item in horarios_aux)
            {
                DatatableHorario horario = new DatatableHorario();
                horario.nombre = item.Nombre;
                horario.id = item.Id;
                if (item.ControlId == 1)
                {
                    horario.lunes=this.PorDefecto();
                    horario.martes=this.PorDefecto();
                    horario.miercoles=this.PorDefecto();
                    horario.jueves=this.PorDefecto();
                    horario.viernes=this.PorDefecto();
                    horario.sabado=this.PorDefecto();
                    horario.domingo=this.PorDefecto();
                }
                else
                {
                    foreach (var dia in item.Dias)
                    {
                        switch (dia.Nombre)
                        {
                            case "lunes":
                                horario.lunes = $"{dia.Start.ToString("HH:mm")} - {dia.End.ToString("HH:mm")}";
                                break;
                            case "martes":
                                horario.martes = $"{dia.Start.ToString("HH:mm")} - {dia.End.ToString("HH:mm")}";
                                break;
                            case "miercoles":
                                horario.miercoles = $"{dia.Start.ToString("HH:mm")} - {dia.End.ToString("HH:mm")}";
                                break;
                            case "jueves":
                                horario.jueves = $"{dia.Start.ToString("HH:mm")} - {dia.End.ToString("HH:mm")}";
                                break;
                            case "viernes":
                                horario.viernes = $"{dia.Start.ToString("HH:mm")} - {dia.End.ToString("HH:mm")}";
                                break;
                            case "sabado":
                                horario.sabado = $"{dia.Start.ToString("HH:mm")} - {dia.End.ToString("HH:mm")}";
                                break;
                            case "domingo":
                                horario.domingo = $"{dia.Start.ToString("HH:mm")} - {dia.End.ToString("HH:mm")}";
                                break;
                            default:

                                break;
                        }
                    }
                }

                horarios.Add(horario);
            }
            recordsTotal = horarios.Count();
            horarios = horarios.Skip(skip).Take(pageSize).ToList();
            return new
            {
                draw = "draw",
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = horarios
            };
        }
        private string PorDefecto()
        {
            return "00:00 - 23:59";
        }
        public async Task<Horario> store(Horario horario)
        {
            this._dbContext.Horario.Add(horario);
            await _dbContext.SaveChangesAsync();
            return await this._dbContext.Horario.Where(h=>h.Id==horario.Id).Include(x=>x.Dias).FirstOrDefaultAsync();
        }
        public async Task<Horario> update(Horario horario)
        {
            _dbContext.Entry(await _dbContext.Horario.FirstOrDefaultAsync(x => x.Id == horario.Id)).CurrentValues.SetValues(new
            {
                Id = horario.Id,
                Nombre = horario.Nombre,
                Descripcion = horario.Nombre
            });
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Horario.Where(p => p.Id == horario.Id).Include(x => x.Dias).FirstAsync();
        }
        public async Task<Horario> UpdateControlId(Horario horario)
        {
            _dbContext.Entry(await _dbContext.Horario.FirstOrDefaultAsync(x => x.Id == horario.Id)).CurrentValues.SetValues(new
            { 
                Id = horario.Id,
                ControlIdName = horario.Nombre,
                ControlId = horario.ControlId
            });
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Horario.Where(p => p.Id == horario.Id).Include(x => x.Dias).FirstAsync();
        }
        public async Task<bool> StoreAll(List<Horario> horarios)
        {
            await _dbContext.AddRangeAsync(horarios);
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
        public async Task<List<Horario>> GetAllByID(List<int> horarios_id)
        {
            var horarios = await this._dbContext.Horario.Where(horario => horarios_id.Contains(horario.Id)).ToListAsync();
            return horarios;
        }
        public async Task<Horario> SearchControlId(int ControlId)
        {
            return await _dbContext.Horario.Where(p => p.ControlId == ControlId).FirstOrDefaultAsync();
        }
        public async Task<Horario> GetOne(int horarios_id)
        {
            return await _dbContext.Horario.Include(h => h.Dias).Where(h => h.Id == horarios_id).FirstOrDefaultAsync();
        }
        public async Task<bool> DeleteDias(int horario_id)
        {
            var dias = await _dbContext.Dia.Where(x => x.HorarioId == horario_id).ToListAsync();
            if (dias.Count > 0)
            {
                _dbContext.Dia.RemoveRange(dias);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Delete(int horario_id)
        {
            var dia = await _dbContext.Horario.Where(x => x.Id == horario_id).FirstOrDefaultAsync();
            if (dia != null)
            {
                _dbContext.Dia.RemoveRange(dia.Dias);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}