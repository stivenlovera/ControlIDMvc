using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class Area
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public int ControlId { get; set; }
        public string ControlIdName { get; set; }
        public List<AreaReglaAcceso> AreaReglaAcceso { get; set; }
    }
}