using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Models.DatatableModel
{
    public class DatatablePlanCuentas
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombreCuenta { get; set; }
        public decimal moneda { get; set; }
        public decimal valor { get; set; }
        public string nivel { get; set; }
        public string modalEdit { get; set; }
        public string modalCreate { get; set; }
        public decimal debe { get; set; }
        public decimal haber { get; set; }
    }
}