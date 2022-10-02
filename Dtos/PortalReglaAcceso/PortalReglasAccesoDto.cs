using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.PortalReglaAcceso
{
    public class PortalReglasAccesoDto
    {
        public int Id { get; set; }
        public int PortalId { get; set; }
        public int ControlIdPortalId { get; set; }
        public int ControlIdRulesId { get; set; }
        public int ReglaAccesoId { get; set; }
    }
}