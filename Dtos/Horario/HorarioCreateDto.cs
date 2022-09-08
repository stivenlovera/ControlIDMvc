using System.ComponentModel.DataAnnotations;
using ControlIDMvc.Dtos.Dia;

namespace ControlIDMvc.Dtos.Horario
{
    public class HorarioCreateDto
    {
        [Required(ErrorMessage = "Nombre requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Descripcion requerido")]
        public string Descripcion { get; set; }
        public List<DiaCreateDto> Dias { get; set; }
    }
}