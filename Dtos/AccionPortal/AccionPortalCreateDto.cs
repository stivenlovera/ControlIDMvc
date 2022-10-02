using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.AccionPortal
{
    public class AccionPortalCreateDto
    {
        public int ControlIdPortalId { get; set; }
        public int portalId { get; set; }
        public int AccionId { get; set; }
        public int ControlIdAreaId { get; set; }
    }
}