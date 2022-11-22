using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Models.DatatableModel
{
    public class DatatablePlanCuentas
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string NombreCuenta { get; set; }
        public decimal Moneda { get; set; }
        public decimal Valor { get; set; }
        public string Nivel { get; set; }
        public string ModalEdit { get; set; }
        public string ModalCreate { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
    }
}