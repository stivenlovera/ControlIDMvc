using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Egreso
{
    public class EgresoCreateDto
    {
        [Required(ErrorMessage = "Numero recibo es requerido")]
        public string NumeroRecibo { get; set; }
        [Required(ErrorMessage = "Fecha Creacion es requerido")]
        public DateTime FechaCreacion { get; set; }
        [Required(ErrorMessage = "Monto es requerido")]
        public decimal Monto { get; set; }
        [Required(ErrorMessage = "Concepto es requerido")]
        public string Concepto { get; set; }
        [Required(ErrorMessage = "Persona es requerido")]
        public string Persona { get; set; }
        [Required(ErrorMessage = "Nombre usuario es requerido")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "Usuario es requerido")]
        public int UsuarioId { get; set; }
    }
}