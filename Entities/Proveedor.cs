using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Nit { get; set; }
        public string CI { get; set; }
        public string Telefono { get; set; }
        public string Dirrecion { get; set; }
    }
}