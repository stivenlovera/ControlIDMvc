using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Models
{
    public class Recibo
    {
        public DateTime Fecha { get; set; }
        public string Nombres { get; set; }
        public decimal Costo { get; set; }
    }
}