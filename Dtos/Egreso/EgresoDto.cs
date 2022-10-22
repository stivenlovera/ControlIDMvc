using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Egreso
{
    public class EgresoDto
    {
        public int Id { get; set; }
        public string NumeroRecibo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal Monto { get; set; }
        public string Concepto { get; set; }
        public string Persona { get; set; }
        public int UsuarioId { get; set; }
    }
}