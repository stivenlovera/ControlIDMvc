using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Paquete
{
    public class PaqueteCreateDto
    {
        [Required(ErrorMessage = "Nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Numero de Dias es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Cantidad de dias debe de ser numerico")]
        public int Dias { get; set; }
        public DateTime FechaCreacion { get; set; }
        [Required(ErrorMessage = "Costo es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Costo debe de ser numerico")]
        public double? Costo { get; set; }
    }
}