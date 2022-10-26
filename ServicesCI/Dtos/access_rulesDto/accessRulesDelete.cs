using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.access_rulesDto
{
    public class accessRulesDelete
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class access_rulesResponseDeleteDto
    {
        public int changes { get; set; }
    }
}