using System.ComponentModel.DataAnnotations;
using ControlIDMvc.Dtos.Dia;

namespace ControlIDMvc.Dtos.Horario
{
    public class HorarioCreateDto
    {
        [Required(ErrorMessage = "Nombre requerido")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ControlId { get; set; }
        public List<string> Dias { get; set; }
        public List<string> DiasControlId { get; set; }
        public List<string> Hora_inicio { get; set; }
        public List<string> Hora_fin { get; set; }
    }
}