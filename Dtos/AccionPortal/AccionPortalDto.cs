using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.AccionPortal
{
    public class AccionPortalDto
    {
        public int Id { get; set; }
        public int ControlIdPortalId { get; set; }
        public int portalId { get; set; }
        public int AccionId { get; set; }
        public int ControlActionId { get; set; }
    }
}