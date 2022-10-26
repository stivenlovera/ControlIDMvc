using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.portalsAccessRulesDto
{
    public class portalAccesoRulesUpdate
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class where
    {
        public portal_access_rules portal_access_rules { get; set; }
    }
    public class values
    {
        public int portal_id { get; set; }
        public int access_rule_id { get; set; }
    }
    public class portal_access_rules
    {
        public int id { get; set; }
    }
    public class portalAccesoRulesResponseUpdateDto
    {
        public int changes { get; set; }
    }
}