using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.user_access_rules
{
    public class userAccessRulesCreateDto
    {
        public int user_id { get; set; }
        public int access_rule_id { get; set; }
    }
    public class responseUserAccessRulesCreateDto
    {
        public List<int> ids { get; set; }
    }
}