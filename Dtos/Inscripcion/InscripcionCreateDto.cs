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
        [Required(ErrorMessage = "Numero de recibo es requerido")]
        public string NumeroRecibo { get; set; }
        [Required(ErrorMessage = "Fecha Final es requerido")]
        public DateTime? FechaFin { get; set; }
        [Required(ErrorMessage = "Costo es requerido")]
        public decimal? Costo { get; set; }
        [Required(ErrorMessage = "Seleccione un paquete")]
        public int PaqueteId { get; set; }
        [Required(ErrorMessage = "Seleccione una persona")]
        public int? PersonaId { get; set; }
        [Required(ErrorMessage = "Nombre es requerido")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Apellido es requerido")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Nombre CI es requerido")]
        public string CI { get; set; }
        [Required(ErrorMessage = "Nombre cliente es requerido")]
        public int? Dias { get; set; }
    }
}