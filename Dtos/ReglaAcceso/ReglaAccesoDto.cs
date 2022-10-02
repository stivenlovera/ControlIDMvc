using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.ReglaAcceso
{
    public class ReglaAccesoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ControlIdName { get; set; }
        public int ControlIdType { get; set; }
        public int ControlIdPriority { get; set; }
    }
}