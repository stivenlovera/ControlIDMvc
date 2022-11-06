using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.user_access_rules
{
    public class userAccessRulesUpdateDto
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class where
    {
        public user_access_rules users { get; set; }
    }
    public class values
    {
        public long user_id { get; set; }
        public long access_rule_id { get; set; }
    }
    public class user_access_rules
    {
        public int id { get; set; }
    }
    public class userAccessRulesResponseUpdateDto
    {
        public int changes { get; set; }
    }
}