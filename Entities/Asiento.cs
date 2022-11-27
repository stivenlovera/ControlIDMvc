using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class Asiento
    {
        public int Id { get; set; }
        public string NombreAsiento { get; set; }
        [Column(TypeName = "decimal(20,2)")]
        public decimal Monto { get; set; }
        public Factura Factura { get; set; }
        public int MovimientosAsientoId { get; set; }
        public MovimientosAsiento MovimientosAsiento { get; set; }
        public List<PlanAsiento> PlanAsientos { get; set; }
    }
}