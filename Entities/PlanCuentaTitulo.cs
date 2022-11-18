using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class PlanCuentaTitulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string NombreCuenta { get; set; }
        [Column(TypeName = "decimal(20,2)")]
        public decimal Moneda { get; set; }
        [Column(TypeName = "decimal(20,2)")]
        public decimal Valor { get; set; }
        public string Nivel { get; set; }
        [Column(TypeName = "decimal(20,2)")]
        public decimal Debe { get; set; }
        [Column(TypeName = "decimal(20,2)")]
        public decimal Haber { get; set; }
        public int PlanCuentaRubroId { get; set; }
        public PlanCuentaRubro PlanCuentaRubro { get; set; }
        public List<PlanCuentaCompuesta> PlanCuentaCompuesta { get; set; }

    }
}