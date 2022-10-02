using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Inscripcion
{
    public class InscripcionCreateDto
    {
        public DateTime FechaCreacion { get; set; }
        [Required(ErrorMessage = "Fecha Inicio es requerido")]
        public DateTime? FechaInicio { get; set; }
        [Required(ErrorMessage = "Fecha Final es requerido")]
        public DateTime? FechaFin { get; set; }
        [Required(ErrorMessage = "Costo es requerido")]
        public double? Costo { get; set; }
        [Required(ErrorMessage = "Seleccione un costo")]
        public int PaqueteId { get; set; }
        [Required(ErrorMessage = "Seleccione una persona")]
        public int? PersonaId { get; set; }
        [Required(ErrorMessage = "Nombre liente es requerido")]
        public string Cliente { get; set; }
        [Required(ErrorMessage = "Dias es requerido")]
        public int? Dias { get; set; }
    }
}