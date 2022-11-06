using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.user_access_rules
{
    public class userAccessRulesDeleteDto
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class userAccessRuleResponseDeleteDto
    {
        public int changes { get; set; }
    }
}