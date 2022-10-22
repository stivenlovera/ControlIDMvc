using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class Caja
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string NumeroRecibo { get; set; }
        public string Concepto { get; set; }
        public string Persona { get; set; }
        public string Tipo { get; set; }
        [Column(TypeName = "decimal(20,2)")]
        public decimal Valor { get; set; }
    }
}