using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Models.DatatableModel
{
    public class DataTableEgreso
    {
        public int Id { get; set; }
        public string FechaCreacion { get; set; }
        public decimal Monto { get; set; }
        public string NumeroRecibo { get; set; }
        public string Concepto { get; set; }
        public string Usuario { get; set; }
    }
}