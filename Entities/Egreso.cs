using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class Egreso
    {
        public int Id { get; set; }
        public string NumeroRecibo { get; set; }
        public DateTime FechaCreacion { get; set; }
        [Column(TypeName = "decimal(20,2)")]
        public decimal Monto { get; set; }
        public string Concepto { get; set; }
        public string Persona { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}