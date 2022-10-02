using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.PortalReglaAcceso
{
    public class PortalReglaAccesoCreateDto
    {
        public int PortalId { get; set; }
        public int ControlIdPortalId { get; set; }
        public int ControlIdRulesId { get; set; }
        public int ReglaAccesoId { get; set; }
    }
}