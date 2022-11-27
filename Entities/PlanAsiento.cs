using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class PlanAsiento
    {
        public int Id { get; set; }
        public string PlanCuenta { get; set; }
        public string PlanId { get; set; }
        [Column(TypeName = "decimal(20,2)")]
        public decimal Debe { get; set; }
        [Column(TypeName = "decimal(20,2)")]
        public decimal Haber { get; set; }
        public int AsientoId { get; set; }
        public Asiento Asiento { get; set; }
    }
}