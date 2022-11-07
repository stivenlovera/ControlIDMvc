using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class PlanCuentaGrupo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string NombreCuenta { get; set; }
        [Column(TypeName = "decimal(20,2)")]
        public decimal Moneda { get; set; }
        public decimal Valor { get; set; }
        public string Nivel { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
        public List<PlanCuentaRubro> PlanCuentaRubros { get; set; }
    }
}