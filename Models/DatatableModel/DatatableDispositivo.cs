using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Models.DatatableModel
{
    public class DatatableDispositivo
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int controlId { get; set; }
        public string controlIdName { get; set; }
        public string controlIdIp { get; set; }
        public string controlIdPublicKey { get; set; }
    }
}