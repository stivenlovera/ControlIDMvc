using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class PlanCuentaRubro
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
        public int PlanCuentaGrupoId { get; set; }
        public PlanCuentaGrupo PlanCuentaGrupo { get; set; }
        public List<PlanCuentaTitulo> PlanCuentaTitulo { get; set; }
    }
}