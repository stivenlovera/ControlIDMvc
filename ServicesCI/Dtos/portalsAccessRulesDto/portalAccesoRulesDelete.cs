using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.portalsAccessRulesDto
{
    public class portalAccesoRulesDelete
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class portalAccesoRulesResponseDeleteDto
    {
        public int changes { get; set; }
    }
}