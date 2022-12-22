using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Models.DatatableModel
{
    public class DatatableMovimiento
    {
        public int id { get; set; }
        public string usuario { get; set; }
        public string numeroRecibo { get; set; }
        public string fecha { get; set; }
        public string tipoMovimiento { get; set; }
        public string receptor { get; set; }
        public decimal montoTotal { get; set; }
    }
}