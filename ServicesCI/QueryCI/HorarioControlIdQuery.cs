using ControlIDMvc.ServicesCI.Dtos.time_spansDto;
using ControlIDMvc.ServicesCI.Dtos.time_zonesDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    /*crear horario en la api*/
    public class HorarioControlIdQuery
    {
        public string ApiUrl { get; set; }
        public BodyCreateObject CreateDias(List<time_spansCreateDto> tiempos, int horario)
        {
            foreach (var tiempo in tiempos)
            {
                tiempo.time_zone_id = horario;
            }
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "time_spans",
                values = tiempos
            };
            return body;
        }
        public BodyCreateObject CreateHorario(time_zonesCreateDto horario)
        {
            List<time_zonesCreateDto> horarios=new List<time_zonesCreateDto>();
            horarios.Add(horario);
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "time_zones",
                values = horarios
            };
            return body;
        }
    }
}