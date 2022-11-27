using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Models.DatatableModel
{
    public class DatatableMovimiento
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string NumeroRecibo { get; set; }
        public string Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public string Receptor { get; set; }
        public decimal MontoTotal { get; set; }
    }
}