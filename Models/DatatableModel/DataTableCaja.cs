using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Models.DatatableModel
{
    public class DataTableCaja
    {
        public int Id { get; set; }
        public string Fecha { get; set; }
        public string NumeroRecibo { get; set; }
        public string Entregado { get; set; }
        public string Usuario { get; set; }
        public string Concepto { get; set; }
        public string Tipo { get; set; }
        public decimal Egreso { get; set; }
        public decimal Ingreso { get; set; }
    }
}