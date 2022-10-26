using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class AccionPortal
    {
        public int Id { get; set; }
        public int ControlIdPortalId { get; set; }
        public int portalId { get; set; }
        public Portal Area { get; set; }
        public int AccionId { get; set; }
        public int ControlActionId { get; set; }
        public Accion Puerta { get; set; }
    }
}