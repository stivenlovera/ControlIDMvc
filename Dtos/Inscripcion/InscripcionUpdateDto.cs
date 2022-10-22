using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ControlIDMvc.Dtos.Inscripcion
{
    public class InscripcionUpdateDto
    {
        public int Id { get; set; }
        public string NumeroRecibo { get; set; }
        public DateTime FechaCreacion { get; set; }
        [Required(ErrorMessage = "Fecha Inicio es requerido")]
        public DateTime FechaInicio { get; set; }
        [Required(ErrorMessage = "Fecha Final es requerido")]
        public DateTime FechaFin { get; set; }
        [Required(ErrorMessage = "Costo es requerido")]
        public decimal? Costo { get; set; }
        [Range(1, 200, ErrorMessage = "Please select a country")]
        [Required(ErrorMessage = "Seleccione un paquete")]
        public int PaqueteId { get; set; }
        [Required(ErrorMessage = "Seleccione una persona")]
        public string PaqueteNombre { get; set; }
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