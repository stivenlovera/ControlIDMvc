using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.access_rulesDto
{
    public class accessRulesUpdate
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class where
    {
        public access_rules access_rules { get; set; }
    }
    public class values
    {
        public string name { get; set; }
        public int type { get; set; }
        public int priority { get; set; }

    }
    public class access_rules
    {
        public int id { get; set; }
    }
    public class access_rulesResponseUpdateDto
    {
        public int changes { get; set; }
    }
}