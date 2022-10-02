using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class Accion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int ControlId { get; set; }
        public string ControlIdName { get; set; }
        public string ControlIdAction { get; set; }
        public string ControlIdParametrers { get; set; }
        public int ControlIdRunAt { get; set; }
        public List<AccionPortal> PortalAccion { get; set; }
    }
}