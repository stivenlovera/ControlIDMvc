using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.portalsAccessRulesDto
{
    public class portalAccesoRulesDto
    {
        public int portal_id { get; set; }
        public int access_rule_id { get; set; }
    }
    public class portalAccesoRulesResponseDto
    {
        public List<portalAccesoRulesDto> portalAccesoRulesDtos { get; set; }
    }
}