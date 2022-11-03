using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Horario
{
    public class HorarioDto
    {
        [Required(ErrorMessage = "Nombre requerido")]
        public int Id { get; set; }
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