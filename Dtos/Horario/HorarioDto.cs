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
        public int ControlId { get; set; }
        public List<string> Dias { get; set; }
        public List<string> DiasControlId { get; set; }
        public List<string> Hora_inicio { get; set; }
        public List<string> Hora_fin { get; set; }
    }
}