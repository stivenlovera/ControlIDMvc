using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class Factura
    {
        public int Id { get; set; }
        public int NroFactura { get; set; }
        public int AsientoId { get; set; }
        public Asiento Asiento { get; set; }
    }
}