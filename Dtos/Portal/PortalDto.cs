using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Portal
{
    public class PortalDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ControlId { get; set; }
        public string ControlIdName { get; set; }
        public int ControlIdAreaFromId { get; set; }
        public int ControlIdAreaToId { get; set; }
    }
}