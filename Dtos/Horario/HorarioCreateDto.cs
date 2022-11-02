using System.ComponentModel.DataAnnotations;
using ControlIDMvc.Dtos.Dia;

namespace ControlIDMvc.Dtos.Horario
{
    public class HorarioCreateDto
    {
        [Required(ErrorMessage = "Nombre requerido")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string lunes { get; set; }
        public string martes { get; set; }
        public string miercoles { get; set; }
        public string jueves { get; set; }
        public string viernes { get; set; }
        public string sabado { get; set; }
        public string domingo { get; set; }

        public DateTime hora_inicio_lunes { get; set; }
        public DateTime hora_inicio_martes { get; set; }
        public DateTime hora_inicio_miercoles { get; set; }
        public DateTime hora_inicio_jueves { get; set; }
        public DateTime hora_inicio_viernes { get; set; }
        public DateTime hora_inicio_sabado { get; set; }
        public DateTime hora_inicio_domingo { get; set; }

        public DateTime hora_fin_lunes { get; set; }
        public DateTime hora_fin_martes { get; set; }
        public DateTime hora_fin_miercoles { get; set; }
        public DateTime hora_fin_jueves { get; set; }
        public DateTime hora_fin_viernes { get; set; }
        public DateTime hora_fin_sabado { get; set; }
        public DateTime hora_fin_domingo { get; set; }

    }
}